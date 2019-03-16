using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InGame.Core.Entities;
using InGame.Web.UI.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TokenOptions = InGame.Api.Extensions.TokenOptions;

namespace InGame.Api.Controllers
{

    //[Route("[controller]/[action]")]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private IConfiguration _config;
        private readonly TokenOptions _tokenOptions;

        public TokenController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            //IEmailSender emailSender,
            ILogger<TokenController> logger,
            IConfiguration config, IOptions<TokenOptions> tokens)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_emailSender = emailSender;
            _logger = logger;
            _config = config;
            _tokenOptions = tokens.Value;
        }

        [TempData] public string ErrorMessage { get; set; }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Generate([FromBody] LoginViewModel model)
        {
            IActionResult response = Unauthorized();

            if (!ModelState.IsValid) return BadRequest("Could not create token");

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null) return BadRequest("Could not create token");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return BadRequest("Could not create token");

            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Issuer,
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
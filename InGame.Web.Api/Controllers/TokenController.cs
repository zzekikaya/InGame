using System.Collections.Generic;
using System.Security.Claims;
using InGame.Core.Entities;
using InGame.Web.UI.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InGame.Wep.Api.Controllers
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

        public TokenController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<TokenController> logger,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _config = config;
        }

        [TempData] public string ErrorMessage { get; set; }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();
            
            
            var result = _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);
            if (result.Result.Succeeded)
            {

               
                ApplicationUser userInfo =  _userManager.FindByEmailAsync(login.Username).Result;
                var identity =  _userManager.GetClaimsAsync(userInfo);
                BuildToken buildToken = new BuildToken(_config);
                string token = buildToken.CreateToken(userInfo);
          
                //_logger.LogInformation("User logged in.");
                _userManager.GetClaimsAsync(userInfo);
                response = Ok(new { token = token });

            }


            return response;
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
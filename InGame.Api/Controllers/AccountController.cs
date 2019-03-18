using InGame.Api.Models;
using InGame.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TokenOptions = InGame.Api.Extensions.TokenOptions;

namespace InGame.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private IConfiguration _config;
        private readonly TokenOptions _tokenOptions;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<TokenController> logger,
            IConfiguration config, IOptions<TokenOptions> tokens)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _config = config;
            _tokenOptions = tokens.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {

                    return BadRequest("Could not find account");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                    $"Please reset your password by using this {code}");
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(model.Email).Result;
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
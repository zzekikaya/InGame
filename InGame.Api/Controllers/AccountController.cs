using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InGame.Api.Models;
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


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
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
                //var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //    $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                //return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            return null;
            // If we got this far, something failed, redisplay form

        }
    }
}
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFN.Api.UI.Base;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.UI.Verify
{
    public class VerifyController : UIController
    {
        public ITransientUserService TransientUserService { get; private set; }
        public IUserService UserService { get; private set; }
        public IPasswordService PasswordService { get; private set; }
        public ILogger Logger { get; private set; }
        public VerifyController(ITransientUserService transientUserService, IUserService userService,
            IPasswordService passwordService, ILogger<VerifyController> logger)
        {
            TransientUserService = transientUserService;
            UserService = userService;
            PasswordService = passwordService;
            Logger = logger;
        }

        [HttpGet("verify/{emailVerificationKey}", Name = "Verify")]
        public async Task<IActionResult> Verify(string emailVerificationKey)
        {
            if (!string.IsNullOrEmpty(emailVerificationKey) && !string.IsNullOrWhiteSpace(emailVerificationKey) &&
                await TransientUserService.EmailVerificationKeyExistsAsync(emailVerificationKey))
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Redirect(AppUrl);
                }
                else
                {
                    var vm = new VerifyViewModel(new VerifyInputModel());
                    vm.EmailVerificationKey = emailVerificationKey;
                    return View(vm);
                }
            }

            return NotFound();
        }

        [HttpPost("verify/{emailVerificationKey}", Name = "Verify")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verify(VerifyInputModel model, string emailVerificationKey)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else if (!await TransientUserService.EmailVerificationKeyExistsAsync(emailVerificationKey))
            {
                return NotFound();
                //return RedirectToAction("VerifyError");
            }
            else if (!PasswordService.ValidatePassword(model.VerifyPassword))
            {
                ModelState.AddModelError("VerifyPassword", "Password must contain at least one number and one letter.");
                var vm = new VerifyViewModel(model);
                return View(vm);
            }

            var transientUser = await TransientUserService.GetByEmailVerificationKeyAsync(emailVerificationKey);
            var bio = new Biography(null,null,null,null);
            var user = new User(transientUser.Username, null, transientUser.Email, null, bio);
            await UserService.CreateAsync(user, model.VerifyPassword);
            await TransientUserService.DeleteAsync(transientUser);

            var claims = UserService.GetClaims(user);
            
            var ci = new ClaimsIdentity(claims, "password", JwtClaimTypes.PreferredUserName,JwtClaimTypes.Role);
            //needed for IDS UI
            ci.AddClaim(new Claim(JwtClaimTypes.Name,user.Username));
            var cp = new ClaimsPrincipal(ci);

            await HttpContext.Authentication.SignInAsync(
                IdentityServer4.IdentityServerConstants.DefaultCookieAuthenticationScheme, cp);
            
            Logger.LogInformation("User Verified and Logged in");

            return View("VerifySuccess");

        }
    }
}
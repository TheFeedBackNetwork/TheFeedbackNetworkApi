using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.UI.Verify
{
    public class VerifyController : Controller
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
            if (!string.IsNullOrEmpty(emailVerificationKey) && string.IsNullOrWhiteSpace(emailVerificationKey) &&
                await TransientUserService.EmailVerificationKeyExistsAsync(emailVerificationKey))
            {
                if (User.Identity.IsAuthenticated)
                {
                    //TODO return to client-app
                    return View();
                }
                else
                {
                    var vm = new VerifyViewModel(new VerifyInputModel());
                    vm.EmailVerificationKey = emailVerificationKey;
                    return View(vm);
                }
            }

            return View();
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
                return RedirectToAction("VerifyError");
            }
            else if (!PasswordService.ValidatePassword(model.VerifyPassword))
            {
                var vm = new VerifyViewModel(new VerifyInputModel());
                return View(vm);
            }

            var transientUser = await TransientUserService.GetByEmailVerificationKeyAsync(emailVerificationKey);
            var bio = new Biography(null,null,null,null);
            var user = new User(transientUser.Username, null, transientUser.Email, null, bio);
            await UserService.CreateAsync(user, model.VerifyPassword);
            await TransientUserService.DeleteAsync(transientUser);

            var claims = UserService.GetClaims(user);
            var ci = new ClaimsIdentity(claims, "password", JwtClaimTypes.PreferredUserName,JwtClaimTypes.Role);
            var cp = new ClaimsPrincipal(ci);

            await HttpContext.Authentication.SignInAsync(
                IdentityServer4.IdentityServerConstants.DefaultCookieAuthenticationScheme, cp);
            
            Logger.LogInformation("User Verified and Logged in");

            return View();

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Services.InMemory;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Models.InputModels;
using TFN.Api.Models.ViewModels;
using TFN.Domain.Interfaces.Services;
using TFN.Mvc.Constants;

namespace TFN.Api.Controllers
{
    public class SignInController : Controller
    {
        public  InMemoryUserLoginService LoginService { get; private set; }
        public IUserService UserService { get; private set; }
        public  IIdentityServerInteractionService Interaction { get; private set; }

        public SignInController(
            InMemoryUserLoginService loginService,
            IUserService userService,
            IIdentityServerInteractionService interaction)
        {
            LoginService = loginService;
            UserService = userService;
            Interaction = interaction;
        }

        [HttpGet]
        [Route(RoutePaths.SignInUrl, Name = "SignIn")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = new SignInViewModel(HttpContext);

            var context = await Interaction.GetAuthorizationContextAsync(returnUrl);
            if (context != null)
            {
                vm.Username = context.LoginHint;
                vm.ReturnUrl = returnUrl;
            }

            return View(vm);
        }

        [HttpPost]
        [Route(RoutePaths.SignInUrl, Name = "SignIn")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInInputModel model)
        {
            if (ModelState.IsValid)
            {
                // validate username/password against in-memory store
                if (await UserService.ValidateCredentialsAsync(model.Username, model.Password))
                {
                    // issue authentication cookie with subject ID and username
                    var user = await UserService.GetAsync(model.Username);
                    await HttpContext.Authentication.SignInAsync(user.Id.ToString(), user.Username);

                    // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint
                    if (Interaction.IsValidReturnUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return Redirect("~/");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            // something went wrong, show form with error
            var vm = new SignInViewModel(HttpContext, model);
            return View(vm);
        }

        //was account/external && returnUrl was /account/externalcallback?
        [HttpGet]
        [Route(RoutePaths.SignInUrl + "/external", Name = "ExternalSignIn")]
        public IActionResult External(string provider, string returnUrl)
        {
            if (returnUrl != null)
            {
                returnUrl = UrlEncoder.Default.Encode(returnUrl);
            }
            returnUrl = "/signin/externalcallback?returnUrl=" + returnUrl;

            // start challenge and roundtrip the return URL
            return new ChallengeResult(provider, new AuthenticationProperties
            {
                RedirectUri = returnUrl
            });
        }

        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        [Route(RoutePaths.SignInUrl + "/externalcallback", Name = "ExternalSignInCallback")]
        public async Task<IActionResult> ExternalCallback(string returnUrl)
        {
            // read external identity from the temporary cookie
            var tempUser = await HttpContext.Authentication.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (tempUser == null)
            {
                throw new Exception("External authentication error");
            }

            // retrieve claims of the external user
            var claims = tempUser.Claims.ToList();

            // try to determine the unique id of the external user - the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            if (userIdClaim == null)
            {
                userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            }
            if (userIdClaim == null)
            {
                throw new Exception("Unknown userid");
            }

            // remove the user id claim from the claims collection and move to the userId property
            // also set the name of the external authentication provider
            claims.Remove(userIdClaim);
            var provider = userIdClaim.Issuer;
            var userId = userIdClaim.Value;

            // check if the external user is already provisioned
            var user = await UserService.FindByExternalProviderAsync(provider, userId);
            if (user == null)
            {
                // this sample simply auto-provisions new external user
                // another common approach is to start a registrations workflow first
                user = await UserService.AutoProvisionUserAsync(provider, userId, claims);
            }

            var additionalClaims = new List<Claim>();

            // if the external system sent a session id claim, copy it over
            var sid = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                additionalClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            // issue authentication cookie for user
            await HttpContext.Authentication.SignInAsync(user.Id.ToString(), user.Username, provider, additionalClaims.ToArray());

            // delete temporary cookie used during external authentication
            await HttpContext.Authentication.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // validate return URL and redirect back to authorization endpoint
            if (Interaction.IsValidReturnUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("~/");

        }
    }
}

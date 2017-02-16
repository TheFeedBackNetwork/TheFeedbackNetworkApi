using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using TFN.Mvc.Constants;
using TFN.Api.UI.Base;

namespace TFN.Api.UI.SignOut
{
    public class SignOutController : UIController
    {
        public  IIdentityServerInteractionService Interaction { get; private set; }

        public SignOutController( IIdentityServerInteractionService interaction)
        {
            Interaction = interaction;
        }

        [HttpGet]
        [Route(RoutePaths.SignOutUrl, Name = "SignOut")]
        public IActionResult SignOut(string logoutId)
        {
            var vm = new SignOutViewModel
            {
                SignOutId = logoutId
            };

            return View(vm);
        }


        [HttpPost]
        [Route(RoutePaths.SignOutUrl, Name = "SignOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut(SignOutViewModel model)
        {
            // delete authentication cookie
            await HttpContext.Authentication.SignOutAsync();

            // set this so UI rendering sees an anonymous user
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await Interaction.GetLogoutContextAsync(model.SignOutId);

            //might need for app redirecturi
            /*var vm = new SignedOutViewModel
            {
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = logout?.ClientId,
                SignOutIframeUrl = logout?.SignOutIFrameUrl
            };

            if (vm.PostLogoutRedirectUri == null)
            {
                vm.PostLogoutRedirectUri = AppUrl;
            }

            return View("SignedOut", vm);*/
            return RedirectToAction("SignIn", "SignIn");
        }
    }
}

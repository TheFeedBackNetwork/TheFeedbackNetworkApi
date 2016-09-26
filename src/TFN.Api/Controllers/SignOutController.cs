using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Models.ViewModels;
using Microsoft.AspNetCore.Http.Authentication;
using TFN.Mvc.Constants;

namespace TFN.Api.Controllers
{
    public class SignOutController : Controller
    {
        public  IIdentityServerInteractionService Interaction { get; private set; }

        public SignOutController( IIdentityServerInteractionService interaction)
        {
            Interaction = interaction;
        }

        [HttpGet]
        [Route(RoutePaths.SignOutUrl, Name = "SignOut")]
        public IActionResult Logout(string logoutId)
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
        public async Task<IActionResult> Logout(SignOutViewModel model)
        {
            // delete authentication cookie
            await HttpContext.Authentication.SignOutAsync();

            // set this so UI rendering sees an anonymous user
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await Interaction.GetLogoutContextAsync(model.SignOutId);

            var vm = new SignedOutViewModel
            {
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = logout?.ClientId,
                SignOutIframeUrl = logout?.SignOutIFrameUrl
            };

            return View("SignedOut", vm);
        }
    }
}

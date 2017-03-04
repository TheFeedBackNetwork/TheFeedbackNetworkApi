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
        public async Task<IActionResult> SignOut(string logoutId)
        {
            var vm = new SignOutViewModel
            {
                SignOutId = logoutId
            };

            return await SignOut(vm);
        }


        [HttpPost]
        [Route(RoutePaths.SignOutUrl, Name = "SignOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut(SignOutViewModel model)
        {
            await HttpContext.Authentication.SignOutAsync();

            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            var logout = await Interaction.GetLogoutContextAsync(model.SignOutId);

            return Redirect(AppUrl);

        }
    }
}

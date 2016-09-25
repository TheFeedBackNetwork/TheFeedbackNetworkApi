using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using TFN.Mvc.Constants;

namespace TheFeedBackNetworkApi.UI.Error
{
    public class ErrorController : Controller
    {
        private readonly IUserInteractionService Interaction;

        public ErrorController(IUserInteractionService interaction)
        {
            Interaction = interaction;
        }

        [Route(RoutePaths.ErrorUrl, Name ="Error")]
        public async Task<IActionResult> Index(string errorId)
        {
            var vm = new ErrorViewModel();

            if (errorId != null)
            {
                var message = await Interaction.GetErrorContextAsync(errorId);
                if (message != null)
                {
                    vm.Error = message;
                }
            }

            return View("Error", vm);
        }
    }
}

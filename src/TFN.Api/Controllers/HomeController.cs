using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TFN.Api.Models.ViewModels;

namespace TFN.Api.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService Interaction;

        public HomeController(IIdentityServerInteractionService interaction)
        {
            Interaction = interaction;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            var message = await Interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}
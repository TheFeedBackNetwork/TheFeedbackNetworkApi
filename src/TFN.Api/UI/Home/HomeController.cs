using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheFeedBackNetworkApi.UI.Home
{
    public class HomeController : Controller
    {
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
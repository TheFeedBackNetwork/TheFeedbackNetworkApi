using Microsoft.AspNetCore.Mvc;

namespace TFN.UI.Home
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
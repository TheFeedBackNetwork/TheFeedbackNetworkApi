using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TFN.Api.UI.PrivacyPolicy
{
    public class PrivacyPolicyController : Controller
    {
        [HttpGet]
        [Route("privacy-policy", Name = "PrivacyPolicy")]
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}

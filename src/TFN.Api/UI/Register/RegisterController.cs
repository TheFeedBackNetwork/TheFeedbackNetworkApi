using Microsoft.AspNetCore.Mvc;
using TFN.Api.UI.Base;

namespace TFN.Api.UI.Register
{
    public class RegisterController : UIController
    {
        [HttpGet("register", Name = "Register")]
        public IActionResult Register()
        {
            
        }
    }
}
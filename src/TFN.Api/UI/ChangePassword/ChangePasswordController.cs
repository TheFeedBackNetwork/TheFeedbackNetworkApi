using Microsoft.AspNetCore.Mvc;
using TFN.Api.UI.Base;
using TFN.Domain.Interfaces.Services;

namespace TFN.Api.UI.ChangePassword
{
    public class ChangePasswordController : UIController
    {

        public IAccountEmailService AccountEmailService { get; private set; }
        public ChangePasswordController(IAccountEmailService accountEmailService)
        {
            AccountEmailService = accountEmailService;
        }

        [HttpGet]
        [Route("changepassword", Name = "ChangePassword")]
        public IActionResult ChangePassword()
        {
            var vm = new ChangePasswordViewModel(new ChangePasswordInputModel());

            return View(vm);
        }
    }
}

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
        [Route("changepassword/{changePasswordKey}", Name = "ChangePassword")]
        public IActionResult ChangePassword(string changePasswordKey)
        {
            var vm = new ChangePasswordViewModel(new ChangePasswordInputModel());

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("changepassword/{changePasswordKey}", Name = "ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel model, string changePasswordKey)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ConfirmPassword", "Invalid password details. Please try again.");
                return View(new ChangePasswordViewModel(model));
            }
            if (!model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPassword", "Password and confirmation need to match. Please try again.");
                return View(new ChangePasswordViewModel(model));
            }
            else
            {
                return RedirectToAction("ChangePasswordSuccess");
            }
        }

        [HttpGet]
        [Route("changepassword/success", Name = "ChangePasswordSuccess")]
        public IActionResult ChangePasswordSuccess()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            return View();
        }
    }
}

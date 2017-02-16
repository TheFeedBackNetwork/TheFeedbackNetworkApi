using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.UI.Base;
using TFN.Domain.Interfaces.Services;

namespace TFN.Api.UI.ChangePassword
{
    public class ChangePasswordController : UIController
    {

        public IAccountEmailService AccountEmailService { get; private set; }
        public IUserService UserService { get; private set; }
        public IPasswordService PasswordService { get; private set; }
        public ChangePasswordController(IUserService userService, IAccountEmailService accountEmailService, IPasswordService passwordService)
        {
            AccountEmailService = accountEmailService;
            UserService = userService;
            PasswordService = passwordService;
        }

        [HttpGet]
        [Route("changepassword/{changePasswordKey}", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword(string changePasswordKey)
        {
            if (!await UserService.ChangePasswordKeyExistsAsync(changePasswordKey))
            {
                return NotFound();
            }

            var vm = new ChangePasswordViewModel(new ChangePasswordInputModel());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("changepassword/{changePasswordKey}", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, string changePasswordKey)
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
            else if(!PasswordService.ValidatePassword(model.ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPassword", "Password needs to be 6 characters long with at least one number.");
                return View(new ChangePasswordViewModel(model));
            }
            else
            {
                var user = await UserService.GetByChangePasswordKey(changePasswordKey);
                if (user == null)
                {
                    //TODO Correct error handling
                    return NotFound();
                }

                await UserService.UpdateUserPasswordAsync(changePasswordKey, model.ConfirmPassword);

                return View("ChangePasswordSuccess");
            }
        }

        [HttpGet]
        [Route("changepassword/success", Name = "ChangePasswordSuccess")]
        public IActionResult ChangePasswordSuccess()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View(AppUrl);
            }

            return View("ChangePasswordSuccess");
        }
    }
}

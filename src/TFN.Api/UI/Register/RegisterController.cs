using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.UI.Base;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;

namespace TFN.Api.UI.Register
{
    public class RegisterController : UIController
    {
        public ITransientUserService TransientUserService { get; private set; }
        public IUserService UserService { get; private set; }
        public IKeyService KeyService { get; private set; }
        public RegisterController(ITransientUserService transientUserService, IUserService userService,
            IKeyService keyService)
        {
            TransientUserService = transientUserService;
            UserService = userService;
            KeyService = keyService;
        }

        [HttpGet("register", Name = "Register")]
        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var vm = new RegisterViewModel();
                return View(vm);
            }

            return View();
        }

        [HttpPost("register", Name = "Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new RegisterViewModel(model));
            }

            if (await UserService.ExistsByEmail(model.RegisterEmail))
            {
                ModelState.AddModelError("email","This email has already been used.");
                return View(new RegisterViewModel(model));
            }

            if (await UserService.ExistsByUsername(model.RegisterUsername))
            {
                ModelState.AddModelError("username", "This username has already been used.");
                return View(new RegisterViewModel(model));
            }

            if (!await CanTrasientUserTakeUsername(model.RegisterEmail, model.RegisterUsername))
            {
                ModelState.AddModelError("username", "This username has already been used.");
                return View(new RegisterViewModel(model));
            }

            var key = KeyService.GenerateUrlSafeUniqueKey();

            var transientUser = new TransientUser(model.RegisterUsername,model.RegisterEmail,key);

            await TransientUserService.CreateAsync(transientUser);

            return View("RegisterConfirmed");


        }

        private async Task<bool> CanTrasientUserTakeUsername(string email, string username)
        {
            var transientUserByUsername = await TransientUserService.GetByUsernameAsync(username);

            if (transientUserByUsername != null)
            {
                if (transientUserByUsername.Email.Equals(email))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
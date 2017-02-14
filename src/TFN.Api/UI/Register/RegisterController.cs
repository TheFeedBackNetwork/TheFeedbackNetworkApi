using System;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.UI.Base;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;

namespace TFN.Api.UI.Register
{
    public class RegisterController : UIController
    {
        public ITransientUserRepository TransientUserRepository { get; private set; }
        public IUserService UserService { get; private set; }
        public IKeyService KeyService { get; private set; }

        public RegisterController(ITransientUserRepository transientUserRepository, IUserService userService,
            IKeyService keyService)
        {
            TransientUserRepository = transientUserRepository;
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
        public IActionResult Register(RegisterInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new RegisterViewModel(model));
            }
            throw new NotImplementedException();
            //if(UserService.)
        }
    }
}
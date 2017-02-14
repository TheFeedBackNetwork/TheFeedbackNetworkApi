using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;

namespace TFN.Api.UI.VerifyAccount
{
    [Route("verify")]
    public class VerifyController : Controller
    {
        public ITransientUserService TransientUserService { get; private set; }
        public IUserService UserService { get; private set; }
        public IPasswordService PasswordService { get; private set; }
        public VerifyController(ITransientUserService transientUserService, IUserService userService,
            IPasswordService passwordService)
        {
            TransientUserService = transientUserService;
            UserService = userService;
            PasswordService = passwordService;
        }

        [HttpGet("verify/{emailVerificationKey}", Name = "Verify")]
        public async Task<IActionResult> VerifyAsync(string emailVerificationKey)
        {
            if (!string.IsNullOrEmpty(emailVerificationKey) && string.IsNullOrWhiteSpace(emailVerificationKey) &&
                await TransientUserService.VerificationKeyExistsAsync(emailVerificationKey))
            {
                if (User.Identity.IsAuthenticated)
                {
                    return View();
                }
                else
                {
                    var vm = new VerifyViewModel(new VerifyInputModel());
                    vm.EmailVerificationKey = emailVerificationKey;
                    return View(vm);
                }
            }

            return View();
        }
    }
}
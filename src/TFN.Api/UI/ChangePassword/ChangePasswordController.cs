using Microsoft.AspNetCore.Mvc;
using TFN.Domain.Interfaces.Services;

namespace TFN.Api.UI.ChangePassword
{
    [Route("api/changepassword")]
    public class ChangePasswordController : Controller
    {
        public IAccountEmailService EmailService { get; private set; }
        public ChangePasswordController(IAccountEmailService emailService)
        {
            EmailService = emailService;
        }

        [HttpGet]
        public IActionResult Post()
        {
            EmailService.SendForgotPasswordEmailAsync("lutz@whereismytransport.com", "afasgag");
            EmailService.SendVerificationEmailAsync("lutando@ngqakaza.com", "AAASSSS");
            //EmailService.SendEmailAsync("lutz@whereismytransport.com", "test", "lol");
            return Ok("ayylmao");
        }
    }
}

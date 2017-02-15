using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Options;

namespace TFN.Domain.Services
{
    public class AccountEmailService : IAccountEmailService
    {
        public IEmailService EmailService { get; private set; }
        public string KeyBaseUrl { get; private set; }
        public AccountEmailService(IOptions<AccountEmailServiceOptions> options,IEmailService emailService)
        {
            EmailService = emailService;
            KeyBaseUrl = options.Value.KeyBaseUrl;
        }
        public async Task SendForgotPasswordEmailAsync(string toEmail, string token)
        {
            await EmailService.SendEmailAsync(toEmail, "Password Reset for The Feedback Network", $"Hi, click on the link to reset your password {KeyBaseUrl}/forgot/{token}");
        }

        public async Task SendVerificationEmailAsync(string toEmail,string username, string token)
        {
            await EmailService.SendEmailAsync(toEmail, "Thanks for joining The Feedback Network", $"Hi {username} </br> click on the link to complete registration {KeyBaseUrl}/verify/{token}");

        }
    }
}
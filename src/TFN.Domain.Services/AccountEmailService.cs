using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Options;

namespace TFN.Domain.Services
{
    public class AccountEmailService : IAccountEmailService
    {
        public IEmailService EmailService { get; private set; }
        public string VerificationKeyBaseUrl { get; private set; }
        public AccountEmailService(IOptions<AccountEmailServiceOptions> options,IEmailService emailService)
        {
            EmailService = emailService;
            VerificationKeyBaseUrl = options.Value.VerificationKeyBaseUrl;
        }
        public Task SendForgotPasswordEmailAsync(string toEmail, string token)
        {
            throw new NotImplementedException();
        }

        public Task SendVerificationEmailAsync(string toEmail, string token)
        {
            throw new NotImplementedException();
        }
    }
}
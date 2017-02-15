using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Services
{
    public interface IAccountEmailService
    {
        Task SendForgotPasswordEmailAsync(string toEmail, string token);
        Task SendVerificationEmailAsync(string toEmail, string token);
    }
}
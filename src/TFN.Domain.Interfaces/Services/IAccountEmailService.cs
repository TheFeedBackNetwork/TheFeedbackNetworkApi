using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Services
{
    public interface IAccountEmailService
    {
        Task SendChangePasswordEmailAsync(string toEmail, string token);
        Task SendVerificationEmailAsync(string toEmail, string username, string token);
    }
}
using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
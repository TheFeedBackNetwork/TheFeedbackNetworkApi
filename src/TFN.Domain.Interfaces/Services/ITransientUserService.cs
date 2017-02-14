using System.Threading.Tasks;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface ITransientUserService
    {
        Task CreateAsync(TransientUser transientUser);
        Task<bool> VerificationKeyExistsAsync(string verificationKey);
        Task DeleteAsync(TransientUser transientUser);
    }
}
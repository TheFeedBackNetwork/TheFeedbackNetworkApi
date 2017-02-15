using System.Threading.Tasks;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface ITransientUserService
    {
        Task CreateAsync(TransientUser transientUser);
        Task<bool> EmailVerificationKeyExistsAsync(string emailVerificationKey);
        Task DeleteAsync(TransientUser transientUser);
        Task<TransientUser> GetByEmailVerificationKeyAsync(string emailVerificationKey);
    }
}
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface IUserService
    {  
        Task<User> AutoProvisionUserAsync(string provider, string userId, List<Claim> claims);
        Task<bool> ValidateCredentialsAsync(string usernameOrEmail, string password);
        bool ValidateUsernameCharacterSafety(string password);
        Task<User> FindByExternalProviderAsync(string provider, string userId);
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsByUsername(string username);
        Task CreateAsync(User user, string password);
        IEnumerable<Claim> GetClaims(User user);
        Task SendChangePasswordKeyAsync(User user);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetAsync(string usernameOrEmail, string password);
        Task<User> GetByChangePasswordKey(string changePasswordKey);
        Task<bool> ChangePasswordKeyExistsAsync(string changePasswordKey);
        Task UpdateUserPasswordAsync(string changePasswordKey, string password);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Extensions;
using TFN.Domain.Services.Utilities;

namespace TFN.Domain.Services
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; private set; }
        public IKeyService KeyService { get; private set; }
        public IAccountEmailService AccountEmailService { get; private set; }
        public ICreditService CreditService { get; private set; }
        public UserService(IUserRepository userRepository, IKeyService keyService, IAccountEmailService accountEmailService, ICreditService creditService)
        {
            UserRepository = userRepository;
            KeyService = keyService;
            AccountEmailService = accountEmailService;
            CreditService = creditService;
        }

        public async Task CreateAsync(User user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(user)}");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException($"{nameof(password)}");
            }

            var credit = new Credits(user.Id,user.Username);
            await CreditService.AddAsync(credit);
            await UserRepository.AddAsync(user, password);
        }

        public async Task DeleteAsync(Guid id)
        {
            await UserRepository.DeleteAsync(id);
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await UserRepository.GetAsync(id);

            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await UserRepository.GetByUsernameAsync(username);

            return user;
        }

        public async Task<User> GetAsync(string usernameOrEmail, string password)
        {
            User user = null;

            if (usernameOrEmail.IsEmail())
            {
                user = await UserRepository.GetByEmailAsync(usernameOrEmail, password);
            }
            else if(usernameOrEmail.IsValidUsername())
            {
                user = await UserRepository.GetByUsernameAsync(usernameOrEmail, password);
            }

            return user;
        }

        public async Task UpdateAsync(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }

            await UserRepository.UpdateAsync(entity);
        }
        

        public IEnumerable<Claim> GetClaims(User user)
        {
            var claims = ClaimsUtility.GetClaims(user);

            return claims;
        }

        public async Task<bool> ValidateCredentialsAsync(string usernameOrEmail, string password)
        {
            
            var user = await GetAsync(usernameOrEmail, password);

            return user != null;
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            var user = await UserRepository.GetByEmailAsync(email);

            return user != null;
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            var user = await UserRepository.GetByUsernameAsync(username);

            return user != null;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await UserRepository.GetByEmailAsync(email);

            return user;
        }

        public bool ValidateUsernameCharacterSafety(string username)
        {
            const string invalidUrlCharacters = "!&$+,/.;=?@#'<>[]{}()|\\^%\"*";

            if (username == null)
            {
                return false;
            }

            if (invalidUrlCharacters.Any(c => username.Contains(c.ToString())))
            {
                return false;
            }

            return true;
        }
        public async Task SendChangePasswordKeyAsync(User user)
        {
            var forgotPasswordKey = KeyService.GenerateUrlSafeUniqueKey();
            await UserRepository.UpdateChangePasswordKeyAsync(user, forgotPasswordKey);
            await AccountEmailService.SendChangePasswordEmailAsync(user.Email, forgotPasswordKey);
        }
        public async Task<bool> ChangePasswordKeyExistsAsync(string changePasswordKey)
        {
            return await UserRepository.ChangePasswordKeyExistsAsync(changePasswordKey);
        }

        #pragma warning disable 1998
        //TODO Remove when we async
        public async Task<User> FindByExternalProviderAsync(string provider, string userId)
        {
            throw new NotImplementedException();
        }
        #pragma warning disable 1998
        //TODO Remove when we async
        public async Task<User> AutoProvisionUserAsync(string provider, string userId, List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserPasswordAsync(string changePasswordKey, string password)
        {
            var user = await UserRepository.GetByChangePasswordKey(changePasswordKey);
            await UserRepository.UpdateUserPasswordAsync(user, password);
        }

        public async Task<User> GetByChangePasswordKey(string changePasswordKey)
        {
            return await UserRepository.GetByChangePasswordKey(changePasswordKey);
        }
    }
}

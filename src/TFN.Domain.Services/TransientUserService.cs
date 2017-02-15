using System;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Services
{
    public class TransientUserService : ITransientUserService
    {
        public ITransientUserRepository TransientUserRepository { get; private set; }
        public IAccountEmailService AccountEmailService { get; private set; }
        public TransientUserService(ITransientUserRepository transientUserRepository,
            IAccountEmailService accountEmailService)
        {
            TransientUserRepository = transientUserRepository;
            AccountEmailService = accountEmailService;
        }
        public async Task CreateAsync(TransientUser transientUser)
        {
            if (transientUser == null)
            {
                throw new ArgumentNullException(nameof(transientUser));
            }

            await TransientUserRepository.AddAsync(transientUser);
            await AccountEmailService.SendVerificationEmailAsync(transientUser.Email, transientUser.EmailVerificationKey);

        }

        public async Task DeleteAsync(TransientUser transientUser)
        {
            await TransientUserRepository.DeleteAsync(transientUser.Id);
        }

        public async Task<bool> EmailVerificationKeyExistsAsync(string emailVerificationKey)
        {
            var transientUser = await TransientUserRepository.GetByEmailVerificationKeyAsync(emailVerificationKey);

            return transientUser != null;
        }

        public async Task<TransientUser> GetByEmailVerificationKeyAsync(string emailVerificationKey)
        {
            return await TransientUserRepository.GetByEmailVerificationKeyAsync(emailVerificationKey);
        }

        public async Task<TransientUser> GetByEmailAsync(string email)
        {
            return await TransientUserRepository.GetByEmailAsync(email);
        }

        public async Task<TransientUser> GetByUsernameAsync(string username)
        {
            return await TransientUserRepository.GetByUsernameAsync(username);
        }
    }
}
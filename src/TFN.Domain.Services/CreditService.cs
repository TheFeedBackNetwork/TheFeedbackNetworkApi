using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Services
{
    public class CreditService : ICreditService
    {
        public ICreditRepository CreditRepository { get; private set; }
        public CreditService(ICreditRepository creditRepository)
        {
            CreditRepository = creditRepository;
        }

        public async Task AwardCreditAsync(Guid fromUserId, Guid toUserId, int amount)
        {
            var credits = await CreditRepository.GetByUserId(toUserId);
            if (credits == null)
            {
                throw new ArgumentException($"{nameof(credits)} cannot be null.");
            }

            var newCredits = credits.ChangeTotalCredits(amount);
            await CreditRepository.UpdateAsync(newCredits);
        }

        public async Task ReduceCreditsAsync(Credits credits, int amount)
        {
            var newCredits = credits.ChangeTotalCredits(amount);
            await CreditRepository.UpdateAsync(newCredits);
        }

        public async Task<IReadOnlyList<Credits>> GetLeaderBoardAsync(int offset, int limit)
        {
            return await CreditRepository.GetHighestCredits(offset, limit);
        }

        public Task<Credits> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Credits> GetByUserIdAsync(Guid userId)
        {
            return await CreditRepository.GetByUserId(userId);
        }

        public Task<Credits> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
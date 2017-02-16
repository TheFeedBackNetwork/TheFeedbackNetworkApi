﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface ICreditService
    {
        Task AwardCreditAsync(Guid fromUser, Guid toUser, int amount);
        Task ReduceCreditsAsync(Credits credits, int amount);
        Task<IReadOnlyList<Credits>> GetLeaderBoardAsync(int offset, int limit);
        Task<Credits> GetAsync(Guid id);
        Task<Credits> GetByUserIdAsync(Guid userId);
        Task<Credits> GetByUsernameAsync(string username);
    }
}
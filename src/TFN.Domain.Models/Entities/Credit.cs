using System;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Credit : DomainEntity<Guid> , IAggregateRoot
    {
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public int TotalCredits { get; private set; }
        public int IsActive { get; private set; }

        private Credit(Guid id, Guid userId, string username, int totalCredits)
            : base(id)
        {
            UserId = userId;
            Username = username;
            TotalCredits = totalCredits;
        }

        public Credit(Guid userId, string userName)
            : this(Guid.NewGuid(),userId,userName, 10)
        { }

        public static Credit Hydrate(Guid id, Guid userId, string username, int totalCredits)
        {
            return new Credit(id,userId,username,totalCredits);
        }
    }
}
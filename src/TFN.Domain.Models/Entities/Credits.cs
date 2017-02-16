using System;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Credits : DomainEntity<Guid> , IAggregateRoot
    {
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public int TotalCredits { get; private set; }
        public int IsActive { get; private set; }

        private Credits(Guid id, Guid userId, string username, int totalCredits)
            : base(id)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException($"The username [{nameof(username)}] is either null or empty.");
            }
            if (username.Length < 3)
            {
                throw new ArgumentException($"The username [{nameof(username)}] is too short.");
            }
            if (username.Length > 16)
            {
                throw new ArgumentException($"The username [{nameof(username)}] is too long.");
            }
            if (totalCredits < 0)
            {
                throw new ArgumentException($"The total credits [{nameof(totalCredits)}] can not be negative.");
            }
            UserId = userId;
            Username = username;
            TotalCredits = totalCredits;
        }

        public Credits(Guid userId, string userName)
            : this(Guid.NewGuid(),userId,userName, 10)
        { }

        public static Credits Hydrate(Guid id, Guid userId, string username, int totalCredits)
        {
            return new Credits(id,userId,username,totalCredits);
        }

        public Credits ChangeTotalCredits(int amount)
        {
            var newCredits = TotalCredits + amount;
            if (newCredits < 0)
            {
                throw new InvalidOperationException("Credits will result in a negative score");
            }
            return Hydrate(Id,UserId,Username,newCredits);
        }
    }
}
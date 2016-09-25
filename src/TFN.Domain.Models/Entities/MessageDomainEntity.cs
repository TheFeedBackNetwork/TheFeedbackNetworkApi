using System;
using NodaTime;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public abstract class MessageDomainEntity : DomainEntity<Guid>
    {
        public Guid UserId { get; private set; }
        public string Text { get; private set; }
        public bool IsActive { get; private set; }
        public Instant Created { get; private set; }
        public Instant Modified { get; private set; }

        protected MessageDomainEntity(Guid id, Guid userId, string text,bool isActive, Instant created, Instant modified)
            : base(id)
        {
            if(created > modified)
            {
                throw new ArgumentException($"Message created date [{nameof(created)}] cannot exceed the modified date [{nameof(modified)}].");
            }
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException($"The username [{nameof(text)}] is either null or empty.");
            }
            Text = text;
            UserId = userId;
            IsActive = isActive;
            Created = created;
            Modified = modified;
        }
    }
}

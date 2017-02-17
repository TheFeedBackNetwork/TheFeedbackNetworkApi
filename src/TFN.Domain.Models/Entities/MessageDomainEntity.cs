using System;
using NodaTime;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public abstract class MessageDomainEntity : DomainEntity<Guid>
    {
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Text { get; set; }
        public bool IsActive { get; private set; }
        public Instant Created { get; private set; }
        public Instant Modified { get; set; }

        protected MessageDomainEntity(Guid id, Guid userId,string username, string text,bool isActive, Instant created, Instant modified)
            : base(id)
        {
            if(created > modified)
            {
                throw new ArgumentException($"Message created date [{nameof(created)}] cannot exceed the modified date [{nameof(modified)}].");
            }
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException($"The text [{nameof(text)}] is either null or empty.");
            }
            if (text.Length <= 5)
            {
                throw new ArgumentException($"The text [{nameof(text)}] must be longer than 5 characters.");
            }

            Text = text;
            UserId = userId;
            Username = username;
            IsActive = isActive;
            Created = created;
            Modified = modified;
        }
    }
}

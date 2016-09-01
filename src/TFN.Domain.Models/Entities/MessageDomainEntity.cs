using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public abstract class MessageDomainEntity : DomainEntity<Guid>
    {
        public Guid UserId { get; private set; }
        public string Text { get; private set; }      
        public Instant Created { get; private set; }
        public Instant Modified { get; private set; }

        protected MessageDomainEntity(Guid id, Guid userId, string text, Instant created, Instant modified)
            : base(id)
        {
            //TODO Domain Validation

            Text = text;
            UserId = userId;
            Created = created;
            Modified = modified;
        }
    }
}

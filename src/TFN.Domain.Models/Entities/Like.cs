using System;
using NodaTime;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Like : DomainEntity<Guid>
    {
        public Guid PostId { get; private set; }
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public Instant Created { get; private set; }

        private Like(Guid id, Guid postId, Guid userId, string username, Instant created)
            : base(id)
        {
            PostId = postId;
            UserId = userId;
            Username = username;
            Created = created;
        }

        public Like(Guid postId, Guid userId, string username)
            : this(Guid.NewGuid(),postId,userId,username, SystemClock.Instance.Now)
        {
            
        }

        public static Like Hydrate(Guid id, Guid postId, Guid userId, string username, Instant created)
        {
            return new Like(id,postId,userId,username,created);
        }
    }
}
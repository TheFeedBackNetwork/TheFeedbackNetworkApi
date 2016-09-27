using System;
using NodaTime;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Score : DomainEntity<Guid>
    {
        public Guid CommentId { get; private set; }
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public Instant Created { get; private set; }

        private Score(Guid id,Guid commentId, Guid userId,string username, Instant created)
            : base(id)
        {
            CommentId = commentId;
            UserId = userId;
            Username = username;
            Created = created;
        }

        public Score(Guid commentId,Guid userId, string username)
            : this(Guid.NewGuid(),commentId,userId,username,SystemClock.Instance.Now)
        {
        }

        public static Score Hydrate(Guid id,Guid commentId, Guid userId,string username, Instant created)
        {
            return new Score(id,commentId,userId,username,created);
        }
    }
}

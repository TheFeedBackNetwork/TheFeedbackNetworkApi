using System;
using NodaTime;

namespace TFN.Domain.Models.Entities
{
    public class Comment : MessageDomainEntity
    {
        public Guid PostId { get; private set; }
        public int Score { get; private set; }
       
        private  Comment(Guid id,Guid userId,Guid postId, string text, int score, Instant created, Instant modified)
            : base(id,userId,text,created,modified)
        {
            if(score < 0)
            {
                throw new ArgumentException($"Comment score [{nameof(score)}] can not be a negative value.");
            }

            Score = score;
        }

        public Comment(Guid userId,Guid postId, string text, int score, Instant created, Instant modified)
            :this(Guid.NewGuid(), userId,postId, text,score, SystemClock.Instance.Now, SystemClock.Instance.Now)
        {
            
        }

        public static Comment Hydrate(Guid id,Guid userId,Guid postId, string text, int score, Instant created, Instant modified)
        {
            return new Comment(id,userId,postId,text,score,created,modified);
        }

    }
}

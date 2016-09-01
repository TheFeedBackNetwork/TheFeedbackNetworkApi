using System;
using NodaTime;

namespace TFN.Domain.Models.Entities
{
    public class Comment : MessageDomainEntity
    {
        public int Score { get; private set; }
       
        private  Comment(Guid id,Guid userId, string text, int score, Instant created, Instant modified)
            : base(id,userId,text,created,modified)
        {
            //TODO Domain Validation

            Score = score;
        }

        public Comment(Guid userId, string text, int score, Instant created, Instant modified)
            :this(Guid.NewGuid(), userId, text,score,created,modified)
        {
            
        }

        public static Comment Hydrate(Guid id,Guid userId, string text, int score, Instant created, Instant modified)
        {
            return new Comment(id,userId,text,score,created,modified);
        }

    }
}

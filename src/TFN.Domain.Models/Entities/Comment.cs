using System;
using System.Collections.Generic;
using NodaTime;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Models.Entities
{
    public class Comment : MessageDomainEntity
    {
        public Guid PostId { get; private set; }
        public IReadOnlyList<Score> Scores { get; private set; }
       
        private  Comment(Guid id,Guid userId,Guid postId, string text, IReadOnlyList<Score> scores, Instant created, Instant modified)
            : base(id,userId,text,created,modified)
        {
            PostId = postId;
            Scores = scores;
        }

        public Comment(Guid userId,Guid postId, string text, IReadOnlyList<Score> scores, Instant created, Instant modified)
            :this(Guid.NewGuid(), userId,postId, text,scores, SystemClock.Instance.Now, SystemClock.Instance.Now)
        {
            
        }

        public static Comment Hydrate(Guid id,Guid userId,Guid postId, string text, IReadOnlyList<Score> scores, Instant created, Instant modified)
        {
            return new Comment(id,userId,postId,text,scores,created,modified);
        }

    }
}

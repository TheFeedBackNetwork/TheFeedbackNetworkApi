using System;
using System.Collections.Generic;
using NodaTime;

namespace TFN.Domain.Models.Entities
{
    public class Comment : MessageDomainEntity
    {
        public Guid PostId { get; private set; }
        public IReadOnlyList<Score> Scores { get; private set; }
       
        private  Comment(Guid id,Guid userId,Guid postId,string username, string text, IReadOnlyList<Score> scores,bool isActive, Instant created, Instant modified)
            : base(id,userId,username,text,isActive,created,modified)
        {
            PostId = postId;
            Scores = scores;
        }

        public Comment(Guid userId,Guid postId,string username, string text, IReadOnlyList<Score> scores)
            :this(Guid.NewGuid(), userId,postId,username, text,scores,true, SystemClock.Instance.Now, SystemClock.Instance.Now)
        {
            
        }

        public static Comment Hydrate(Guid id,Guid userId,Guid postId,string username, string text, IReadOnlyList<Score> scores,bool isActive, Instant created, Instant modified)
        {
            return new Comment(id,userId,postId,username,text,scores,isActive,created,modified);
        }

    }
}

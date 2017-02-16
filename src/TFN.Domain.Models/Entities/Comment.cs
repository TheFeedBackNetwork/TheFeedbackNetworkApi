using System;
using NodaTime;

namespace TFN.Domain.Models.Entities
{
    public class Comment : MessageDomainEntity
    {
        public Guid PostId { get; private set; }
       
        private  Comment(Guid id, Guid userId, Guid postId, string username, string text, bool isActive, Instant created, Instant modified)
            : base(id,userId,username,text,isActive,created,modified)
        {
            PostId = postId;
        }

        public Comment(Guid userId,Guid postId,string username, string text)
            :this(Guid.NewGuid(), userId,postId,username, text, true, SystemClock.Instance.Now, SystemClock.Instance.Now)
        {
            
        }

        public static Comment Hydrate(Guid id, Guid userId, Guid postId, string username, string text, bool isActive, Instant created, Instant modified)
        {
            return new Comment(id,userId,postId,username,text,isActive,created,modified);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;

namespace TFN.Domain.Models.Entities
{
    public class Comment : MessageDomainEntity
    {
        //TODO Change IList to IReadOnlyList Scores & private setter
        public Guid PostId { get; private set; }
        public IList<Score> Scores { get; private set; }
       
        private  Comment(Guid id,Guid userId,Guid postId,string username, string text, IReadOnlyList<Score> scores,bool isActive, Instant created, Instant modified)
            : base(id,userId,username,text,isActive,created,modified)
        {
            PostId = postId;
            Scores = scores.ToList();
        }

        public Comment(Guid userId,Guid postId,string username, string text)
            :this(Guid.NewGuid(), userId,postId,username, text,new List<Score>(), true, SystemClock.Instance.Now, SystemClock.Instance.Now)
        {
            
        }

        public static Comment Hydrate(Guid id,Guid userId,Guid postId,string username, string text, IReadOnlyList<Score> scores,bool isActive, Instant created, Instant modified)
        {
            return new Comment(id,userId,postId,username,text,scores,isActive,created,modified);
        }

        public void EditComment(string text)
        {
            Text = text;
            Modified = SystemClock.Instance.Now;
        }

    }
}

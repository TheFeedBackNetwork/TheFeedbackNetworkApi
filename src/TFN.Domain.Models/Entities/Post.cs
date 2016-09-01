using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Post : MessageDomainEntity
    {
        public string TrackUrl { get; private set; }
        public int Likes { get; private set; }
        public IReadOnlyCollection<string> Tags { get; private set; }
        private Post(Guid id, Guid userId, string trackurl, string text,int likes, IReadOnlyCollection<string> tags , Instant created, Instant modified)
            : base(id,userId,text,created,modified)
        {
            //TODO Domain Validation

            TrackUrl = trackurl;
            Likes = likes;
            Tags = tags;
        }

        public Post(Guid userId, string trackurl, string text, int likes, IReadOnlyCollection<string> tags,
            Instant created, Instant modified)
            :this(Guid.NewGuid(), userId,trackurl,text,likes,tags,created,modified)
        {
            
        }

        public static Post Hydrate(Guid id, Guid userId, string trackurl, string text, int likes,
            IReadOnlyCollection<string> tags, Instant created, Instant modified)
        {
            return new Post(id,userId,trackurl,text,likes,tags,created,modified);
        }
    }
}

using System;
using System.Collections.Generic;
using NodaTime;
using TFN.Domain.Models.Extensions;
using TFN.Domain.Models.Enums;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Post : MessageDomainEntity ,IAggregateRoot
    {
        public string TrackUrl { get; private set; }
        public int Likes { get; private set; }
        public IReadOnlyCollection<string> Tags { get; private set; }
        public Genre Genre { get; private set; }
        private Post(Guid id, Guid userId, string trackurl, string text,int likes,Genre genre, IReadOnlyCollection<string> tags , Instant created, Instant modified)
            : base(id,userId,text,created,modified)
        {
            if(likes < 0)
            {
                throw new ArgumentException($"The amount of likes [{nameof(likes)}] can not be a negative value.");
            }
            if(!trackurl.IsTrackUrl())
            {
                throw new ArgumentException($"The url given [{nameof(trackurl)}] is not a valid track Url.");
            }

            TrackUrl = trackurl;
            Likes = likes;
            Genre = genre;
            Tags = tags;
        }

        public Post(Guid userId, string trackurl, string text, int likes, Genre genre, IReadOnlyCollection<string> tags,
            Instant created, Instant modified)
            :this(Guid.NewGuid(), userId,trackurl,text,likes,genre,tags, SystemClock.Instance.Now,SystemClock.Instance.Now)
        {
            
        }

        public static Post Hydrate(Guid id, Guid userId, string trackurl, string text, int likes, Genre genre,
            IReadOnlyCollection<string> tags, Instant created, Instant modified)
        {
            return new Post(id,userId,trackurl,text,likes,genre,tags,created,modified);
        }
    }
}

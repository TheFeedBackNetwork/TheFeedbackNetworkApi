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
        public IReadOnlyList<string> Tags { get; private set; }
        public Genre Genre { get; private set; }
        private Post(Guid id, Guid userId, string username, string trackurl, string text, int likes, Genre genre, IReadOnlyList<string> tags, bool isActive, Instant created, Instant modified)
            : base(id,userId,username,text,isActive,created,modified)
        {
            if(likes < 0)
            {
                throw new ArgumentException($"The amount of likes [{nameof(likes)}] can not be a negative value.");
            }
            if(!trackurl.IsUrl())
            {
                throw new ArgumentException($"The url given [{nameof(trackurl)}] is not a valid track Url.");
            }

            TrackUrl = trackurl;
            Likes = likes;
            Genre = genre;
            Tags = tags;
        }

        public Post(Guid userId,string username, string trackurl, string text, Genre genre, IReadOnlyList<string> tags)
            :this(Guid.NewGuid(), userId,username,trackurl,text,0,genre,tags, true, SystemClock.Instance.Now,SystemClock.Instance.Now)
        {
            
        }

        public static Post Hydrate(Guid id, Guid userId, string username, string trackurl, string text, int likes, Genre genre, IReadOnlyList<string> tags, bool isActive, Instant created, Instant modified)
        {
            return new Post(id,userId,username,trackurl,text,likes,genre,tags,isActive,created,modified);
        }

        public static Post EditPost(Post post, string text, string trackUrl, IReadOnlyList<string> tags, Genre genre)
        {
            return new Post(post.Id, post.UserId, post.Username, trackUrl, text, post.Likes, genre, tags, post.IsActive, post.Created, post.Modified);
        }
    }
}

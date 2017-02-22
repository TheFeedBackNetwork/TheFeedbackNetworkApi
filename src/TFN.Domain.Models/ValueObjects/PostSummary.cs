using System;
using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Models.ValueObjects
{
    public class PostSummary
    {
        public Guid PostId { get; private set; }
        public IReadOnlyList<Like> Likes { get; private set; }
        public int LikeCount { get; private set; }
        public bool AlreadyLiked { get; private set; }

        private PostSummary(Guid postId, IReadOnlyList<Like> likes, int likeCount, bool alreadyLiked)
        {
            if (likes.Count > likeCount)
            {
                throw new ArgumentException($"likes [{nameof(likeCount)}] cannot be less than the summary of posts [{nameof(likes)}]");
            }

            PostId = postId;
            Likes = likes;
            LikeCount = likeCount;
            AlreadyLiked = alreadyLiked;
        }

        public static PostSummary From(Guid postId, IReadOnlyList<Like> likes, int likeCount, bool alreadyLiked)
        {
            return new PostSummary(postId,likes,likeCount,alreadyLiked);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.ResponseModels
{
    public class PostSummaryResponseModel
    {
        public Guid PostId { get; private set; }
        public IReadOnlyList<LikesResponseModel> Likes { get; private set; }
        public int LikeCount { get; private set; }
        public bool AlreadyLiked { get; private set; }

        private PostSummaryResponseModel(Guid postId, IReadOnlyList<LikesResponseModel> likes, int likeCount,
            bool alreadyLiked)
        {
            PostId = postId;
            Likes = likes;
            LikeCount = likeCount;
            AlreadyLiked = alreadyLiked;
        }

        public static PostSummaryResponseModel From(PostSummary postSummary, string apiUrl)
        {
            IReadOnlyList<LikesResponseModel> likes = postSummary.Likes.Select(x => LikesResponseModel.From(x, apiUrl)).ToList();

            return new PostSummaryResponseModel(
                postSummary.PostId,
                likes,
                postSummary.LikeCount,
                postSummary.AlreadyLiked
                );
        }
    }
}
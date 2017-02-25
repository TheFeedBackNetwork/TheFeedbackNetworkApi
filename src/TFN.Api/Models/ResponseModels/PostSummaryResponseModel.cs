using System;
using System.Collections.Generic;
using System.Linq;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.ResponseModels
{
    public class PostSummaryResponseModel
    {
        public Guid PostId { get; private set; }
        public IReadOnlyList<LikesResponseModel> Likes { get; private set; }
        public int LikeCount { get; private set; }
        public bool AlreadyLiked { get; private set; }
        public CreditsResponseModel UserWhoPosted { get; private set; }

        private PostSummaryResponseModel(Guid postId, IReadOnlyList<LikesResponseModel> likes, int likeCount,
            bool alreadyLiked, CreditsResponseModel userWhoPosted)
        {
            PostId = postId;
            Likes = likes;
            LikeCount = likeCount;
            AlreadyLiked = alreadyLiked;
            UserWhoPosted = userWhoPosted;
        }

        public static PostSummaryResponseModel From(PostSummary postSummary,Credits credits, string apiUrl)
        {
            IReadOnlyList<LikesResponseModel> likes = postSummary.Likes.Select(x => LikesResponseModel.From(x, apiUrl)).ToList();
            var creditsSummary = CreditsResponseModel.From(credits, apiUrl);

            return new PostSummaryResponseModel(
                postSummary.PostId,
                likes,
                postSummary.LikeCount,
                postSummary.AlreadyLiked,
                creditsSummary
                );
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.ResponseModels
{
    public class CommentSummaryResponseModel
    {
        public Guid CommentId { get; private set; }
        public IReadOnlyList<ScoreResponseModel> Scores { get; private set; }
        public int ScoreCount { get; private set; }
        public bool AlreadyScored { get; private set; }
        public CreditsResponseModel UserWhoCommented { get; private set; }

        private CommentSummaryResponseModel(Guid commentId, IReadOnlyList<ScoreResponseModel> scores, int scoreCount,
            bool alreadyScored, CreditsResponseModel userWhoCommented)
        {
            CommentId = commentId;
            Scores = scores;
            ScoreCount = scoreCount;
            AlreadyScored = alreadyScored;
            UserWhoCommented = userWhoCommented;
        }

        public static CommentSummaryResponseModel From(CommentSummary commentSummary,Credits credits, string apiUrl, Guid postId)
        {
            IReadOnlyList<ScoreResponseModel> scores =
                commentSummary.Scores.Select(x => ScoreResponseModel.From(x, apiUrl, postId)).ToList();
            var creditsSummary = CreditsResponseModel.From(credits, apiUrl);

            return new CommentSummaryResponseModel(
                commentSummary.CommentId,
                scores,
                commentSummary.ScoreCount,
                commentSummary.AlreadyScored,
                creditsSummary
                );
        }
    }
}

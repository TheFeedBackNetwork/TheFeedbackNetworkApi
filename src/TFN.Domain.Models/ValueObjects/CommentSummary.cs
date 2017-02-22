using System;
using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Models.ValueObjects
{
    public class CommentSummary
    {
        public Guid CommentId { get; private set; }
        public IReadOnlyList<Score> Scores { get; private set; }
        public int ScoreCount { get; private set; }
        public bool AlreadyScored { get; private set; }

        private CommentSummary(Guid commentId, IReadOnlyList<Score> scores, int scoreCount, bool alreadyScored)
        {
            if (scores.Count > scoreCount)
            {
               
                throw new ArgumentException($"score [{nameof(scoreCount)}] cannot be less than the summary of scores [{nameof(scores)}]");
            }

            CommentId = commentId;
            Scores = scores;
            ScoreCount = scoreCount;
            AlreadyScored = alreadyScored;
        }

        public static CommentSummary From(Guid commentId, IReadOnlyList<Score> scores, int scoreCount, bool alreadyScored)
        {
            return new CommentSummary(commentId, scores,scoreCount,alreadyScored);
        }
    }
}
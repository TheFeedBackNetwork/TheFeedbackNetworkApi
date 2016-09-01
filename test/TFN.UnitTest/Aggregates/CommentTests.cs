using NodaTime;
using System;
using TFN.Domain.Models.Entities;
using Xunit;

namespace TFN.UnitTest.Aggregates
{
    public class CommentTests
    {
        private static Guid CommentIdDefaault { get { return new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836"); } }
        private static Guid PostIdDefault { get { return new Guid("2a2c9a98-1853-4405-b41e-ca589a7c243e"); } }
        private static int ScoreDefault { get { return 5; } }
        private static Guid UserIdDefault { get { return new Guid("3d17d22b-9b76-4b2a-aecd-5937f018cda6"); } }
        private static string TextDefault { get { return "This foo is my bar"; } }
        private static Instant CreatedDefault { get { return SystemClock.Instance.Now; } }
        private static Instant ModifiedDefault { get { return SystemClock.Instance.Now; } }

        public Comment make_Comment(Guid id, Guid userId, Guid postId, string text, int score, Instant created, Instant modified)
        {
            return Comment.Hydrate(id, userId, postId, text, score, created, modified);
        }

        public Comment make_Comment(string text)
        {
            return make_Comment(CommentIdDefaault, UserIdDefault, PostIdDefault, text, ScoreDefault, CreatedDefault, ModifiedDefault);
        }

        public Comment make_Comment(int score)
        {
            return make_Comment(CommentIdDefaault, UserIdDefault, PostIdDefault, TextDefault, score, CreatedDefault, ModifiedDefault);
        }

        public Comment make_Comment(Instant created)
        {
            return make_Comment(CommentIdDefaault, UserIdDefault, PostIdDefault, TextDefault, ScoreDefault, created, ModifiedDefault);
        }
    }
}

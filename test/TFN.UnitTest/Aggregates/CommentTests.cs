using NodaTime;
using System;
using System.Collections.Generic;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;
using Xunit;
using FluentAssertions;

namespace TFN.UnitTest.Aggregates
{
    public class CommentTests
    {
        private static Guid CommentIdDefaault { get { return new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836"); } }
        private static Guid PostIdDefault { get { return new Guid("2a2c9a98-1853-4405-b41e-ca589a7c243e"); } }
        private static IReadOnlyList<Score> ScoresDefault { get { return new List<Score> { Score.Hydrate(new Guid("0d7e16cb-372e-4819-add2-79b3095625dc"), new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836"), new Guid("3d17d22b-9b76-4b2a-aecd-5937f018cda6"),"FooBar", SystemClock.Instance.Now) , Score.Hydrate(new Guid("e614380f-547c-4422-acb7-5a8020a16553"), new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836"), new Guid("3d17d22b-9b76-4b2a-aecd-5937f01fcda6"), "FooBarBaz", SystemClock.Instance.Now) }; } }
        private static Guid UserIdDefault { get { return new Guid("3d17d22b-9b76-4b2a-aecd-5937f018cda6"); } }
        private static string UserNameDefault { get { return "FooBar"; } }
        private static string TextDefault { get { return "This foo is my bar"; } }
        private static bool IsActiveDefault { get { return true; } }
        private static Instant CreatedDefault { get { return Instant.FromUtc(2016, 4, 4, 5, 4); } }
        private static Instant ModifiedDefault { get { return Instant.FromUtc(2016, 4, 4, 5, 5); } }

        public Comment make_Comment(Guid id, Guid userId, Guid postId,string username, string text, IReadOnlyList<Score> scores,bool isActive, Instant created, Instant modified)
        {
            return Comment.Hydrate(id, userId, postId,username, text, scores,isActive, created, modified);
        }

        public Comment make_Comment(string text)
        {
            return make_Comment(CommentIdDefaault, UserIdDefault, PostIdDefault,UserNameDefault, text, ScoresDefault,IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        public Comment make_Comment(IReadOnlyList<Score> scores)
        {
            return make_Comment(CommentIdDefaault, UserIdDefault, PostIdDefault, UserNameDefault, TextDefault, scores, IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        public Comment make_Comment(Instant created)
        {
            return make_Comment(CommentIdDefaault, UserIdDefault, PostIdDefault, UserNameDefault, TextDefault, ScoresDefault, IsActiveDefault, created, ModifiedDefault);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(1)]
        public void Constructor_InvalidCreated_ArgumentExceptionThrown(int extraSeconds)
        {
            var instant = SystemClock.Instance.Now.Plus(Duration.FromSeconds(extraSeconds));

            this.Invoking(x => x.make_Comment(instant))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void Constructor_InvalidText_ArgumentNullExceptionThrown(string text)
        {
            this.Invoking(x => x.make_Comment(text))
                .ShouldThrow<ArgumentNullException>();
        }
    }
}

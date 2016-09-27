using NodaTime;
using System;
using System.Collections.Generic;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Enums;
using Xunit;
using FluentAssertions;
using TFN.Domain.Models.ValueObjects;

namespace TFN.UnitTest.Aggregates
{
    public class PostTests
    {
        private static Guid PostIdDefault { get { return new Guid("86bcf89b-6847-4c5d-bcc5-87b69d775e3f"); } }
        private static string PostUserNameDefault { get { return "FooBar"; } }
        private static string CommentUserNameDefault { get { return "BarBaz"; } }
        private static string TrackUrlDefault { get { return "www.soundcloud.com/foo/bar";} }
        private static int LikesDefault { get { return 1; } }
        private static IReadOnlyList<string> TagsDefault { get { return new List<string> { "foo", "bar" }; } }
        private static Genre GenreDefault { get { return Genre.Ambient; } }
        private static Guid UserIdDefault { get { return new Guid("799dca00-ef0f-4f8e-9bd3-5a4cff9ee07e"); } }
        private static IReadOnlyList<Comment> CommentsDefault => new List<Comment> {new Comment(new Guid(),PostIdDefault,CommentUserNameDefault,"foo bar baz",new List<Score> { Score.Hydrate(new Guid("787483ba-2f77-4ab7-9657-cd63b9b2dfbb"), new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836"), new Guid("3d17d22b-9b76-4b2a-aecd-5937f018cda6"), "FooBar", SystemClock.Instance.Now) } )};
        private static string TextDefault { get { return "This bar is my foo."; } }
        private static bool IsActiveDefault { get { return true; } }
        private static Instant CreatedDefault { get { return Instant.FromUtc(2016,4,4,4,4); } }
        private static Instant ModifiedDefault { get { return Instant.FromUtc(2016, 4, 4, 4, 5); } }

        public Post make_Post(Guid id, Guid userId,string username, string trackUrl, string text, int likes, Genre genre, IReadOnlyList<string> tags,IReadOnlyList<Comment> comments,bool isActive,Instant created, Instant modified)
        {
            return Post.Hydrate(id, userId,username, trackUrl, text, likes, genre, tags,comments,isActive, created, modified);
        }

        public Post make_PostByTrackUrl(string trackUrl)
        {
            return Post.Hydrate(PostIdDefault, UserIdDefault,PostUserNameDefault, trackUrl, TextDefault, LikesDefault, GenreDefault, TagsDefault,CommentsDefault,IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        public Post make_PostByText(string text)
        {
            return Post.Hydrate(PostIdDefault, UserIdDefault, PostUserNameDefault, TrackUrlDefault, text, LikesDefault, GenreDefault, TagsDefault, CommentsDefault, IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        public Post make_Post(int likes)
        {
            return Post.Hydrate(PostIdDefault, UserIdDefault, PostUserNameDefault, TrackUrlDefault, TextDefault, likes, GenreDefault, TagsDefault, CommentsDefault, IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        public Post make_Post(Instant created)
        {
            return Post.Hydrate(PostIdDefault, UserIdDefault, PostUserNameDefault, TrackUrlDefault, TextDefault, LikesDefault, GenreDefault, TagsDefault, CommentsDefault, IsActiveDefault, created, ModifiedDefault);
        }

        [Theory]
        [InlineData(10020)]
        [InlineData(1)]
        public void Constructor_InvalidCreated_ArgumentExceptionThrown(int extraSeconds)
        {
            var instant = SystemClock.Instance.Now.Plus(Duration.FromSeconds(extraSeconds));

            this.Invoking(x => x.make_Post(instant))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void Constructor_InvalidText_ArgumentNullExceptionThrown(string text)
        {
            this.Invoking(x => x.make_PostByText(text))
                .ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData("foob")]
        [InlineData("foo")]
        [InlineData("a")]
        public void Constructor_InvalidText_ArgumentExceptionThrown(string text)
        {
            this.Invoking(x => x.make_PostByText(text))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5000)]
        public void Constructor_InvalidLikes_ArgumentExceptionThrown(int likes)
        {
            this.Invoking(x => x.make_Post(likes))
                .ShouldThrow<ArgumentException>();
        }
    }
}

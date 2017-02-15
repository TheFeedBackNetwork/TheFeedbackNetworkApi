using NodaTime;
using System;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;
using Xunit;
using FluentAssertions;

namespace TFN.UnitTest.Aggregates
{
    public class UserTests
    {
        private static Guid UserIdDefault { get { return new Guid("0d7e16cb-372e-4819-add2-79b3095625dc"); } }
        private static string UsernameDefault { get { return "foomusic"; } }
        private static string ProfilePictureUrlDefault { get { return "tfn.foo.bar/picture/foo.png"; } }
        private static string EmailDefault { get { return "foo@bar.com"; } }
        private static string GivenNameDefault { get { return "foo"; } }
        private static string FamilyNameDefault { get { return "bar"; } }
        private static int CreditsDefault { get { return 10; } }
        public static Biography BiographyDefault { get { return Biography.From("FooBar", "www.instagram.com/foo", "www.soundcloud.com/bar", "www.foomusic.net"); } }
        public static Instant CreatedDefault { get { return Instant.FromUtc(2016, 4, 4, 4, 0); } }
        public static bool IsActiveDefault { get { return true;} }

        public User make_User(Guid id, string username,string profilePictureUrl, string email, string givenName, string familyName,int credits, Biography biography, Instant created)
        {
            return User.Hydrate(id, username, profilePictureUrl, email, givenName,credits, biography, created, IsActiveDefault);
        }

        public User make_UserByUsername(string username)
        {
            return make_User(UserIdDefault, username, ProfilePictureUrlDefault, EmailDefault, GivenNameDefault, FamilyNameDefault, CreditsDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByProfilePictureUrl(string profilePictureUrl)
        {
            return make_User(UserIdDefault, UsernameDefault, profilePictureUrl, EmailDefault, GivenNameDefault, FamilyNameDefault, CreditsDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByEmail(string email)
        {
            return make_User(UserIdDefault, UsernameDefault, ProfilePictureUrlDefault, email, GivenNameDefault, FamilyNameDefault, CreditsDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByGivenName(string givenName)
        {
            return make_User(UserIdDefault, UsernameDefault, ProfilePictureUrlDefault, EmailDefault, givenName, FamilyNameDefault, CreditsDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByFamilyName(string familyName)
        {
            return make_User(UserIdDefault, UsernameDefault, ProfilePictureUrlDefault, EmailDefault, GivenNameDefault, familyName, CreditsDefault, BiographyDefault, CreatedDefault);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Constructor_InvalidUserName_ArgumentNullExceptionThrown(string username)
        {

            this.Invoking(x => x.make_UserByUsername(username))
                .ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData("foobarfoobarfoobar")]
        [InlineData("fo")]
        public void Constructor_InvalidUserName_ArgumentExceptionThrown(string username)
        {
            this.Invoking(x => x.make_UserByUsername(username))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("@foo")]
        [InlineData("bar@")]
        [InlineData("@bar.com")]
        [InlineData(" @ ")]
        public void Constructor_InvalidEmail_ArgumentExceptionThrown(string email)
        {
            this.Invoking(x => x.make_UserByEmail(email))
                .ShouldThrow<ArgumentException>();
        }

    }
}

﻿using NodaTime;
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
        public static Biography BiographyDefault { get { return Biography.From("FooBar", "www.instagram.com/foo", "www.soundcloud.com/bar", "www.foomusic.net"); } }
        public static Instant CreatedDefault { get { return SystemClock.Instance.Now; } }

        public User make_User(Guid id, string username,string profilePictureUrl, string email, string givenName, string familyName, Biography biography, Instant created)
        {
            return User.Hydrate(id, username, profilePictureUrl, email, givenName, familyName, biography, created);
        }

        public User make_UserByUsername(string username)
        {
            return make_User(UserIdDefault, username, ProfilePictureUrlDefault, EmailDefault, GivenNameDefault, FamilyNameDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByProfilePictureUrl(string profilePictureUrl)
        {
            return make_User(UserIdDefault, UsernameDefault, profilePictureUrl, EmailDefault, GivenNameDefault, FamilyNameDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByEmail(string email)
        {
            return make_User(UserIdDefault, UsernameDefault, ProfilePictureUrlDefault, email, GivenNameDefault, FamilyNameDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByGivenName(string givenName)
        {
            return make_User(UserIdDefault, UsernameDefault, ProfilePictureUrlDefault, EmailDefault, givenName, FamilyNameDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByFamilyName(string familyName)
        {
            return make_User(UserIdDefault, UsernameDefault, ProfilePictureUrlDefault, EmailDefault, GivenNameDefault, familyName, BiographyDefault, CreatedDefault);
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

        [Theory]
        [InlineData("https://bar.com/foo.png")]
        [InlineData("http://foo.bar.com/foobar.png")]
        public void Constructor_InvalidProfilePictureUrl_ArgumentExceptionThrown(string profilePictureUrl)
        {
            this.Invoking(x => x.make_UserByProfilePictureUrl(profilePictureUrl))
                .ShouldThrow<ArgumentException>();
        }

    }
}
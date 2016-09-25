using System;
using NodaTime;
using TFN.Domain.Models.ValueObjects;
using TFN.DomainDrivenArchitecture.Domain.Models;
using TFN.Domain.Models.Extensions;

namespace TFN.Domain.Models.Entities
{
    public class User : DomainEntity<Guid> , IAggregateRoot
    {
        public string Username { get; private set; }
        public string ProfilePictureUrl { get; private set; } 
        public string Email { get; private set; }
        public string GivenName { get; private set; }
        public string FamilyName { get; private set; }
        public Biography Biography { get; private set; }
        public Instant Created { get; private set; }
        public bool IsActive { get; private set; }

        public User(string username,string profilePictureUrl, string email, string givenName, string familyName, Biography biography)
            : this(Guid.NewGuid(), username,profilePictureUrl, email, givenName, familyName, biography,SystemClock.Instance.Now, true)
        {

        }
        private User(Guid id, string username, string profilePictureUrl, string email, string givenName, string familyName, Biography biography, Instant created, bool isActive)
            : base(id)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException($"The username [{nameof(username)}] is either null or empty.");
            }
            if(username.Length < 3)
            {
                throw new ArgumentException($"The username [{nameof(username)}] is too short.");
            }
            if (username.Length > 16)
            {
                throw new ArgumentException($"The username [{nameof(username)}] is too long.");
            }
            if (!email.IsEmail())
            {
                throw new ArgumentException($"The email [{nameof(email)}] is not a valid email.");
            }
            if(profilePictureUrl.IsUrl())
            {
                throw new ArgumentException($"The profile picture url [{nameof(profilePictureUrl)}] is not a valid profile picture Url.");
            }

            Username = username;
            Email = email;
            ProfilePictureUrl = profilePictureUrl;
            GivenName = givenName;
            FamilyName = familyName;
            Biography = biography;
            Created = created;
            IsActive = isActive;
        }

        public static User Hydrate(Guid id, string username, string profilePictureUrl, string email, string givenName, string familyName, Biography biography,
            Instant created, bool isActive)
        {
            return new User(id, username,profilePictureUrl, email, givenName, familyName, biography, created, isActive);
        }
    }
}

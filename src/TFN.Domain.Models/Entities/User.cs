using System;
using NodaTime;
using TFN.Domain.Models.ValueObjects;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class User : DomainEntity<Guid>
    {
        public string Username { get; private set; }
        public string ProfilePictureUrl { get; private set; } 
        public string Email { get; private set; }
        public string GivenName { get; private set; }
        public string FamilyName { get; private set; }
        public Biography Biography { get; private set; }
        public Instant Created { get; private set; }

        public User(string username,string profilePictureUrl, string email, string givenName, string familyName, Biography biography, Instant created)
            : this(Guid.NewGuid(), username,profilePictureUrl, email, givenName, familyName, biography,created)
        {
            
        }
        private User(Guid id, string username, string profilePictureUrl, string email, string givenName, string familyName, Biography biography, Instant created)
            : base(id)
        {
            //TODO Domain Validation

            Username = username;
            Email = email;
            ProfilePictureUrl = profilePictureUrl;
            GivenName = givenName;
            FamilyName = familyName;
            Biography = biography;
            Created = created;
        }

        public static User Hydrate(Guid id, string username, string profilePictureUrl, string email, string givenName, string familyName, Biography biography,
            Instant created)
        {
            return new User(id, username,profilePictureUrl, email, givenName, familyName, biography, created);
        }
    }
}

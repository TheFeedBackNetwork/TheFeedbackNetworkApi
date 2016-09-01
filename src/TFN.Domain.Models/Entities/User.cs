using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NodaTime;
using TFN.Domain.Models.ValueObjects;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class User : DomainEntity<Guid>
    {
        public string Username { get; private set; }
        public string ProfilePicture { get; private set; } 
        public string Email { get; private set; }
        public string GivenName { get; private set; }
        public string FamilyName { get; private set; }
        public Biography Biography { get; private set; }
        public Instant Created { get; private set; }

        public User(string username,string profilePicture, string email, string givenName, string familyName, Biography biography, Instant created)
            : this(Guid.NewGuid(), username,profilePicture, email, givenName, familyName, biography,created)
        {
            
        }
        private User(Guid id, string username, string profilePicture, string email, string givenName, string familyName, Biography biography, Instant created)
            : base(id)
        {
            //TODO Domain Validation

            Username = username;
            ProfilePicture = profilePicture;
            Email = email;
            GivenName = givenName;
            FamilyName = familyName;
            Biography = biography;
            Created = created;
        }

        public static User Hydrate(Guid id, string username, string profilePicture, string email, string givenName, string familyName, Biography biography,
            Instant created)
        {
            return new User(id, username,profilePicture, email, givenName, familyName, biography, created);
        }
    }
}

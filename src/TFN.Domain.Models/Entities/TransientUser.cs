using System;
using TFN.Domain.Models.Extensions;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class TransientUser : DomainEntity<Guid>, IAggregateRoot
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string EmailVerificationKey { get; private set; }

        public TransientUser(string username, string email, string emailVerificationKey)
            : this(Guid.NewGuid(), username, email, emailVerificationKey)
        {
            
        }

        private TransientUser(Guid id, string username, string email, string emailVerificationKey)
            : base(id)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException($"The username [{nameof(username)}] is either null or empty.");
            }
            if (username.Length < 3)
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
            if (string.IsNullOrWhiteSpace(emailVerificationKey))
            {
                throw new ArgumentNullException($"{nameof(emailVerificationKey)} may not be empty or whitespace.");
            }

            Username = username;
            Email = email;
            EmailVerificationKey = emailVerificationKey;
        }

        public static TransientUser Hydrate(Guid id, string username, string email, string emailVerificationKey)
        {
            return new TransientUser(id,username,email, emailVerificationKey);
        }
    }
}
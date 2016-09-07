using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Domain.Services
{
    public class ProfileService : IProfileService
    {
        private IUserRepository UserRepository { get;set; }
        public ProfileService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            Guid subjectId;

            var validSubject = Guid.TryParse(context.Subject.GetSubjectId(), out subjectId);

            if (!validSubject)
            {
                throw new ArgumentException($"{nameof(context.Subject)} is invalid as a subject Id.");
            }

            var user = await UserRepository.GetAsync(subjectId);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.GivenName, user.GivenName),
                new Claim(JwtClaimTypes.FamilyName, user.FamilyName),
                new Claim(JwtClaimTypes.PreferredUserName, user.Username),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.Picture, user.ProfilePictureUrl),
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            Guid subjectId;

            var validSubject = Guid.TryParse(context.Subject.GetSubjectId(), out subjectId);

            if (!validSubject)
            {
                throw new ArgumentException($"{nameof(context.Subject)} is invalid as a subject Id.");
            }

            var user = await UserRepository.GetAsync(subjectId);

            context.IsActive = (user != null) && user.IsActive;
        }
    }
}

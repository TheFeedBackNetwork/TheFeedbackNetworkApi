using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Services.Utilities;

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

            var claims = ClaimsUtility.GetClaims(user).ToList();

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

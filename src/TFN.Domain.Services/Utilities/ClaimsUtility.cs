using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Services.Utilities
{
    public static class ClaimsUtility
    {
        public static IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.GivenName, user.GivenName),
                new Claim(JwtClaimTypes.FamilyName, user.FamilyName),
                new Claim(JwtClaimTypes.PreferredUserName, user.Username),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.Picture, user.ProfilePictureUrl),
            };

            return claims;
        }
    }
}

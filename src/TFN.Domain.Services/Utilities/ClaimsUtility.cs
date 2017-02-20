using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Services.Utilities
{
    public static class ClaimsUtility
    {
        public static IReadOnlyList<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.PreferredUserName, user.Username),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.EmailVerified, user.Email),
                new Claim(JwtClaimTypes.IdentityProvider, "idsvr"),
                //new Claim(JwtClaimTypes.Name, user.Username) wanna get rid of shit

            };

            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                claims.Add(new Claim(JwtClaimTypes.Name, user.Name));
            }

            if (!string.IsNullOrWhiteSpace(user.ProfilePictureUrl))
            {
                claims.Add(new Claim(JwtClaimTypes.Picture, user.ProfilePictureUrl));
            }

            return claims;
        }
    }
}

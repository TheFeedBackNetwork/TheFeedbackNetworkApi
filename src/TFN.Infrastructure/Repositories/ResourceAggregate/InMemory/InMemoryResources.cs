using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace TFN.Infrastructure.Repositories.ResourceAggregate.InMemory
{
    public class InMemoryResources
    {

        //API Resources
        public static IEnumerable<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource
            {
                Name = "posts.read",
                DisplayName = "Posts Read",
                Description = "Scope for retreiving the posts resource",
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    "biography",
                    "profile_picure_url"
                }
            },
            new ApiResource
            {
                Name = "posts.write",
                DisplayName = "Posts Write",
                Description = "Scope for writing the posts resource",
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    "biography",
                    "profile_picure_url"
                }
            },
            new ApiResource
            {
                Name = "posts.edit",
                DisplayName = "Posts Edit",
                Description = "Scope for editing the posts resource",
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    "biography",
                    "profile_picure_url"
                }
            },
            new ApiResource
            {
                Name = "posts.delete",
                DisplayName = "Posts Edit",
                Description = "Scope for deleting the posts resource",
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    "biography",
                    "profile_picure_url"
                }
            },
            new ApiResource
            {
                Name = "tracks.delete",
                DisplayName = "Tracks Delete",
                Description = "Scope for deleting the tracks resource",
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    "biography",
                    "profile_picure_url"
                }
            },
            new ApiResource
            {
                Name = "tracks.read",
                DisplayName = "Tracks Read",
                Description = "Scope for reading the tracks resource",
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    "biography",
                    "profile_picure_url"
                }
            },
            new ApiResource
            {
                Name = "tracks.write",
                DisplayName = "Tracks Write",
                Description = "Scope for writing the tracks resource",
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    "biography",
                    "profile_picure_url"
                }
            },

        };

        //Identity Resources
        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "profile_picture_url",
                Description = "Non standard identity scope for holding profile picture url"
            },
            new IdentityResource
            {
                Name = "biography",
                Description = "Non standard identity scope for holding biography information",
                UserClaims = new HashSet<string> { "profile_picture_url" }
            }
        };
    }
}
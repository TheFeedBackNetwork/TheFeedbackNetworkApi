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
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "posts.read",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for read",
                        DisplayName = "Posts.Read"
                    }

                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
                }
            },
            new ApiResource
            {
                Name = "posts.write",
                DisplayName = "Posts Write",
                Description = "Scope for writing the posts resource",
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "posts.write",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for writing",
                        DisplayName = "Posts.Write"
                    }

                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
                }
            },
            new ApiResource
            {
                Name = "posts.edit",
                DisplayName = "Posts Edit",
                Description = "Scope for editing the posts resource",
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "posts.edit",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for edit",
                        DisplayName = "Posts.Edit"
                    }

                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
                }
            },
            new ApiResource
            {
                Name = "posts.delete",
                DisplayName = "Posts Delete",
                Description = "Scope for deleting the posts resource",
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "posts.delete",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for deleting",
                        DisplayName = "Posts.Delete"
                    }
                    
                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
                }
            },
            new ApiResource
            {
                Name = "tracks.delete",
                DisplayName = "Tracks Delete",
                Description = "Scope for deleting the tracks resource",
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "tracks.delete",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for deleting tracks",
                        DisplayName = "Tracks.Delete"
                    }

                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
                }
            },
            new ApiResource
            {
                Name = "tracks.read",
                DisplayName = "Tracks Read",
                Description = "Scope for reading the tracks resource",
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "tracks.read",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for reading tracks",
                        DisplayName = "Tracks.Read"
                    }

                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
                }
            },
            new ApiResource
            {
                Name = "tracks.write",
                DisplayName = "Tracks Write",
                Description = "Scope for writing the tracks resource",
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "tracks.write",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for writing tracks",
                        DisplayName = "Tracks.Write"
                    }

                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
                }
            },
            new ApiResource
            {
                Name = "credits.write",
                DisplayName = "Credits Write",
                Description = "Scope for writing the credits resource",
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "credits.write",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for writing credits",
                        DisplayName = "Credits.Write"
                    }

                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
                }
            },
            new ApiResource
            {
                Name = "credits.delete",
                DisplayName = "Credits Delete",
                Description = "Scope for Deleting from the credits resource",
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "credits.delete",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for deleting from credits",
                        DisplayName = "Credits.Delete"
                    }

                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
                }
            },
            new ApiResource
            {
                Name = "credits.read",
                DisplayName = "Credits Read",
                Description = "Scope for reading from the credits resource",
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "credits.read",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for reading credits",
                        DisplayName = "Credits.Read"
                    }

                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
                }
            },
            new ApiResource
            {
                Name = "users.read",
                DisplayName = "Users Read",
                Description = "Scope for reading from the users resource",
                Scopes = new List<Scope>
                {
                    new Scope
                    {
                        Name = "users.read",
                        ShowInDiscoveryDocument = true,
                        Description = "Scope for reading users",
                        DisplayName = "Users.Read"
                    }

                },
                UserClaims =
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.Name,
                    "biography",
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
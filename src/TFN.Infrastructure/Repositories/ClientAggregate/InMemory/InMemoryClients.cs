using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace TFN.Infrastructure.Repositories.ClientAggregate.InMemory
{
    public class InMemoryClients
    {
        public static IEnumerable<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "tfn_postman",
                ClientName = "TFN Postman Client",
                IncludeJwtId = true,
                PrefixClientClaims = true,
                AccessTokenType = AccessTokenType.Jwt,
                AlwaysSendClientClaims = true,
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = new List<string>
                {
                    "openid",
                    "profile",
                    "biography",
                    "profile_picture_url",
                    "posts.write",
                    "posts.read",
                    "posts.edit",
                    "posts.delete"
                }
            },
            new Client
            {
                ClientId = "tfn_frontend",
                ClientName = "TFN Frontent Client",
                IncludeJwtId = true,
                PrefixClientClaims = true,
                AccessTokenType = AccessTokenType.Jwt,
                AllowAccessTokensViaBrowser = true,
                AlwaysSendClientClaims = true,
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                RequireClientSecret = false,
                AllowedScopes = new List<string>
                {
                    "openid",
                    "profile",
                    "biography",
                    "profile_picture_url",
                    "posts.write",
                    "posts.read",
                    "posts.edit",
                    "posts.delete"
                }
            },
        };
    }
}

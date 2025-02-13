﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope{ Name="basicinfo" , DisplayName = "Basic Information"},
                new ApiScope{ Name="newscms" , DisplayName = "News Content Management System"},
            };

    public static IEnumerable<Client> Clients =>
        new Client[]
            { 
                //new Client
                //{
                //    ClientName = "newscmsClient",
                //    ClientId = "newscmsClient",
                //    ClientSecrets = new List<Secret>
                //    {
                //        new Secret("newscmsClient".ToSha256())
                //    },

                //    AllowedGrantTypes = new List<string> { GrantType.ClientCredentials },
                //    AllowedScopes = { "basicinfo", "newscms" }
                //}
                new Client
                {
                    ClientName = "newscmsClient",
                    ClientId = "newscmsClient",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("newscmsClient".ToSha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:7105/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7105/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "basicinfo", "newscms"
                    }
                    ,AllowOfflineAccess = true,
                }
            };
}
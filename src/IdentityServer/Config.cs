using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId()
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
                new Client
                {
                    ClientName = "newscmsClient",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("newscmsClient".ToSha256())
                    },

                    AllowedGrantTypes = new List<string> { GrantType.ClientCredentials },
                    AllowedScopes = { "basicinfo", "newscms" }
                }
            };
}
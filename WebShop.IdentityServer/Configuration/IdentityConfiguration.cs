using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace WebShop.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResources.Profile()
    };
    
    //Definir o escopo
    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        new ApiScope("webshop", "WebShop Server"),
        new ApiScope(name: "read", "Read data."),
        new ApiScope(name: "write", "Write data."),
        new ApiScope(name: "delete", "Delete data.")
    };
    
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            //cliente genérico
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("abracadabra#simsalabim".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials, //precisa das credenciais do usuário
                AllowedScopes = {"read", "write", "profile" }
            },
            new Client
            {
                ClientId = "webshop",
                ClientSecrets = { new Secret("abracadabra#simsalabim".Sha256())},
                AllowedGrantTypes = GrantTypes.Code, //via codigo
                RedirectUris = {"https://localhost:7166/signin-oidc"},//login
                PostLogoutRedirectUris = {"https://localhost:7166/signout-callback-oidc"},//logout
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "webshop"
                }
            }
        };    
}
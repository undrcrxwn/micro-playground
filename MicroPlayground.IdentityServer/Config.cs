using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace MicroPlayground.IdentityServer;

public class Config
{
    public static IEnumerable<Client> Clients => new[]
    {
        new Client
        {
            ClientId = "spa",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
            RedirectUris = new[]
            {
                "https://www.getpostman.com/oauth2/callback"
            },
            ClientSecrets =
            {
                new Secret("secret23456789098745678".Sha256())
            },
            AllowedScopes = { "weather" }
        },
        new Client
        {
            ClientId = "mvc",
            ClientName = "Shopping Web App",
            AllowedGrantTypes = GrantTypes.Hybrid,
            RequirePkce = false,
            AllowRememberConsent = false,
            RedirectUris = new List<string>()
            {
                "http://localhost:5002/signin-oidc"
            },
            PostLogoutRedirectUris = new List<string>()
            {
                "http://localhost:5002/signin-oidc"
            },
            ClientSecrets = new List<Secret>
            {
                new Secret("secret123mvc".Sha256())
            },
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Address,
                IdentityServerConstants.StandardScopes.Email,                           
                "weather",
                "roles"
            }
        }
    };

    public static IEnumerable<ApiScope> ApiScopes => new[]
    {
        new ApiScope("weather", "Weather API")
    };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[] { };

    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Address(),
        new IdentityResources.Email(),
        new("roles", "Your role(s)", new[] { "role" })
    };

    public static List<TestUser> TestUsers => new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "5BE86359–073C-434B-AD2D-A3932222DABE",
            Username = "undrcrxwn",
            Password = "qwerty123",
            Claims = new List<Claim> 
            {
                new(JwtClaimTypes.Name, "SMOLL FLOPPY"),
                new(JwtClaimTypes.Email, "undrcrxwn@gmail.com")
            }
        }
    };
}
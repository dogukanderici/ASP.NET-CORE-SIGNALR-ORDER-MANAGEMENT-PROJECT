

using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SignalR.IdentityServerApi.Models;
using SignalR.IdentityServerApi.Services.Concrete;
using System.Security.Claims;

namespace SignalR.IdentityServerApi
{
    public class Config
    {

        // Her Api için o api'ye erişim sağlayacak kaynak ve o kaynağın kapsamları belirlenir.
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ResourceFullPermission"){Scopes={"ReadPermission","WritePermission","FullPermission","AdminPermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        // Kaynaklarda tanımlanan kapsamların tanımlanması
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("AdminPermission","Full Authority For Admin Operations",new[] { "scope" }),
            new ApiScope("ReadPermission","Full Authority For Read Operations", new[] { "scope" }),
            new ApiScope("WritePermission","Write Authority For Write Operations", new[] { "scope" }),
            new ApiScope("FullPermission","Full Authority For Full Operations", new[] { "scope" }),

            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)

        };

        // Token'ı alınan kullanıcının o token içeriğinde hangi bilgilerine erişim sağlanacağı bildirilir.
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        // Token alacak kullanıcı tiplerinin tanımı yapılır.
        // Her kullanıcı tipi bir ClientId ve ClientSecret ile tanımlanır.

        public static IEnumerable<Client> Clients => new Client[]
        {
            // Customer ( En Alt Yetkideki Kullanıcı ) ( Sisteme Kayıtlı Olmayan Kullanıcı )
            // Müşterilerin sahip olacağı yetkiler

            new Client()
            {
                ClientId="SignalRCustomer",
                ClientName="SignalR Customer User",
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                ClientSecrets={new Secret("signalrcustomersecret".Sha256())},
                AllowedScopes={
                    "ReadPermission",
                    IdentityServerConstants.LocalApi.ScopeName
                }
            },

            // Subscribed Customer ( Sisteme Kayıtlı Müşteri )

            new Client()
            {
                ClientId="SignalRSubscribedCustomer",
                ClientName="SignalR Subscribed Customer",
                AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                ClientSecrets={new Secret("signalrsubscribedcustomersecret".Sha256())},
                AllowedScopes = {
                    "ReadPermission",
                    "WritePermission",
                    //"AdminPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime=3600
            },
            new Client()
            {
                ClientId="SignalRAdmin",
                ClientName="SignalR Administrator",
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                ClientSecrets={new Secret("signalradminsecret".Sha256())},
                AllowedScopes =
                {
                    "ReadPermission",
                    "WritePermission",
                    "FullPermission",
                    "AdminPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime=3600
            }
        };
    }
}
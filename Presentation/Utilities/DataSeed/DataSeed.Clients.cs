using System;
using System.IO;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using Common;

namespace Presentation.Utilities
{

  public partial class DataSeed
  {

    public static string[] _allowedScopes
    {
      get
      {
        return new string[] {
          IdentityServerConstants.StandardScopes.OpenId,
          IdentityServerConstants.StandardScopes.Profile,
          IdentityServerConstants.StandardScopes.Phone,
          IdentityServerConstants.StandardScopes.Email,
          IdentityServerConstants.StandardScopes.Address
        };
      }
    }

    public static IServerUrls ServerUrls { get; set; }
    private static StreamWriter file;

    private static string logoUri = "https://secure.counter-culture.io/android-chrome-512x512.png";
    public static IEnumerable<Client> Clients
    {
      get
      {
        File.Delete("clients.env");
        file = new StreamWriter("clients.env");
        PrintClientInfo(CounterCultureApiInfo);
        PrintClientInfo(CounterCultureAppInfo);
        PrintClientInfo(CounterCultureDevInfo);
        file.Close();
        return new List<Client> {
            new Client {
                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = 86400,
                AllowedScopes = _allowedScopes,
                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientName = CounterCultureApiInfo.Item1,
                ClientId = CounterCultureApiInfo.Item2,
                ClientUri = ServerUrls.API,
                LogoUri = logoUri,
                ClientSecrets = new List<Secret> {
                    new Secret(CounterCultureApiInfo.Item3.Sha512())}
            },
            new Client {
                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = 86400,
                AllowedScopes = _allowedScopes,
                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                ClientName = CounterCultureAppInfo.Item1,
                ClientId = CounterCultureAppInfo.Item2,
                ClientUri = ServerUrls.APP,
                LogoUri = logoUri,
                ClientSecrets = new List<Secret> {
                    new Secret(CounterCultureAppInfo.Item3.Sha512())},
                RedirectUris = {
                  $"{ServerUrls.APP}/oauth2/callback"
                }
            },
            new Client {
                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = 86400,
                AllowedScopes = _allowedScopes,
                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                ClientName = CounterCultureDevInfo.Item1,
                ClientId = CounterCultureDevInfo.Item2,
                ClientUri = ServerUrls.DEV,
                LogoUri = logoUri,
                ClientSecrets = new List<Secret> {
                    new Secret(CounterCultureDevInfo.Item3.Sha512())},
                RedirectUris = {
                  $"{ServerUrls.DEV}/oauth2/callback"
                }
            }
        };
      }
    }

    private static void PrintClientInfo(Tuple<string, string, string> clientInfo){
      file.WriteLine($"\n# {clientInfo.Item1}");
      file.WriteLine($"CLIENT_ID={clientInfo.Item2}");
      file.WriteLine($"CLIENT_SECRET={clientInfo.Item3}\n");
    }

    private static readonly Tuple<string, string, string> CounterCultureApiInfo = new Tuple<string, string, string>(
            "counter-culture.api",
            String.Empty.NewClientId(),
            String.Empty.NewClientSecret());

    private static readonly Tuple<string, string, string> CounterCultureAppInfo = new Tuple<string, string, string>(
            "counter-culture.app",
            String.Empty.NewClientId(),
            String.Empty.NewClientSecret());

    private static readonly Tuple<string, string, string> CounterCultureDevInfo = new Tuple<string, string, string>(
            "counter-culture.dev",
            String.Empty.NewClientId(),
            String.Empty.NewClientSecret());
  }

}

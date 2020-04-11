using System;
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
          IdentityServerConstants.StandardScopes.Email,
          IdentityServerConstants.StandardScopes.Address
        };
      }
    }

    public static IServerUrls ServerUrls { get; set; }
    public static IEnumerable<Client> Clients
    {
      get
      { 

        PrintClientInfo(CounterCultureAppInfo);
        
        PrintClientInfo(CounterCultureDevInfo);

        return new List<Client> {
            new Client {
                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = 86400,
                AllowedScopes = _allowedScopes,
                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                ClientName = CounterCultureAppInfo.Item1,
                ClientId = CounterCultureAppInfo.Item2,
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
      var defaultColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.DarkMagenta;
      Console.WriteLine($"\nClientName: {clientInfo.Item1}");
      Console.WriteLine($"ClientId: {clientInfo.Item2}");
      Console.WriteLine($"ClientSecret: {clientInfo.Item3}");
      Console.ForegroundColor = defaultColor;
    }

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

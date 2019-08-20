using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using CounterCulture.Constants;

namespace CounterCulture.Utilities
{

  public partial class DataSeed
  {

    private static List<string> _allowedScopes
    {
      get
      {
        return new List<string> {
		              "openid",
                  "counters:read",
                  "counters:write",
                  "profile:read",
                  "profile:write"
                };
      }
    }

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
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                ClientName = CounterCultureAppInfo.Item1,
                ClientId = CounterCultureAppInfo.Item2,
                ClientSecrets = new List<Secret> {
                    new Secret(CounterCultureAppInfo.Item3.Sha512())},
                RedirectUris = {
                  $"{ServerUrls.APP[ENV.DEV]}/oauth2/callback",
                  $"{ServerUrls.APP[ENV.PROD]}/oauth2/callback"
                }
            },
            new Client {
                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = 86400,
                AllowedScopes = _allowedScopes,
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                ClientName = CounterCultureDevInfo.Item1,
                ClientId = CounterCultureDevInfo.Item2,
                ClientSecrets = new List<Secret> {
                    new Secret(CounterCultureDevInfo.Item3.Sha512())},
                RedirectUris = {
                  $"{ServerUrls.DEV[ENV.DEV]}/oauth2/callback",
                  $"{ServerUrls.DEV[ENV.PROD]}/oauth2/callback"
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
      Console.WriteLine($"ClientSecret: {clientInfo.Item3}\n");
      Console.ForegroundColor = defaultColor;
    }

    private static Tuple<string, string, string> CounterCultureAppInfo {
      get {
        return new Tuple<string, string, string>(
            "counter-culture.app",
            String.Empty.NewClientId(),
            String.Empty.NewClientSecret());
      }
    }

    private static Tuple<string, string, string> CounterCultureDevInfo {
      get {
        return new Tuple<string, string, string>(
            "counter-culture.dev",
            String.Empty.NewClientId(),
            String.Empty.NewClientSecret());
      }
    }
  }

}

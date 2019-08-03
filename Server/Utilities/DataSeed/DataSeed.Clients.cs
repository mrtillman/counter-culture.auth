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
          return new List<Client> {
              new Client {
                  AccessTokenType = AccessTokenType.Jwt,
                  AccessTokenLifetime = 86400,
                  AllowedScopes = _allowedScopes,
                  AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                  ClientId = "countercultureapp",
                  ClientName = "counter-culture.app",
                  ClientSecrets = new List<Secret> {
                      new Secret("superSecretPassword".Sha512())},
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
                  ClientId = "counterculturedev",
                  ClientName = "counter-culture.dev",
                  ClientSecrets = new List<Secret> {
                      new Secret("superSecretPassword".Sha512())},
                  RedirectUris = {
                    $"{ServerUrls.DEV[ENV.DEV]}/oauth2/callback",
                    $"{ServerUrls.DEV[ENV.PROD]}/oauth2/callback"
                  }
              }
          };
        }
      }
  }

}

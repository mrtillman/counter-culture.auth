using System;
using System.Collections.Generic;
using IdentityServer4.Models;

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
		                "http://localhost:3000/oauth2/callback",
                    "http://localhost:8080",
                    "https://counter-culture.io"
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
                    "http://localhost:9000",
                    "https://geeks.counter-culture.io"
                  }
              }
          };
        }
      }
  }

}

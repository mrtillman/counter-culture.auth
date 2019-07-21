using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace CounterCulture.Utilities
{

  public partial class Juice
  {

    private static List<string> _allowedScopes
    {
      get
      {
        return new List<string> {
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
                  ClientId = "countercultureapp",
                  ClientName = "counter-culture.app",
                  AllowedGrantTypes = GrantTypes.ClientCredentials,
                  ClientSecrets = new List<Secret> {
                      new Secret("superSecretPassword".Sha256())},
                  AllowedScopes = _allowedScopes
              },
              new Client {
                  ClientId = "counterculturedev",
                  ClientName = "counter-culture.dev",
                  AllowedGrantTypes = GrantTypes.ClientCredentials,
                  ClientSecrets = new List<Secret> {
                      new Secret("superSecretPassword".Sha256())},
                  AllowedScopes = _allowedScopes
              }
          };
        }
      }
  }

}
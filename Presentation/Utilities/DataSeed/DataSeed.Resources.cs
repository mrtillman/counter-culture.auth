using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using AspNet.Security.OpenIdConnect.Primitives;

namespace Presentation.Utilities
{
  public partial class DataSeed
  {
    public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource> {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Email(),
              new IdentityResource {
                  Name = "subject",
                  UserClaims = new List<string> {
                      OpenIdConnectConstants.Claims.Subject
                  }
              }
          };

    public static IEnumerable<ApiResource> ApiResources => new List<ApiResource> {
            new ApiResource {
                Name = "read-counters",
                DisplayName = "counters:read",
                Description = "Read access to counter data",
                UserClaims = new List<string> {"role"},
                ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                Scopes = new List<Scope> {
                    new Scope("counters:read")
                }
            },
            new ApiResource {
                Name = "write-counters",
                DisplayName = "counters:write",
                Description = "Write access to counter data",
                UserClaims = new List<string> {"role"},
                ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                Scopes = new List<Scope> {
                    new Scope("counters:write")
                }
            },
            new ApiResource {
                Name = "read-profile",
                DisplayName = "profile:read",
                Description = "Read access to basic profile info",
                UserClaims = new List<string> {"role"},
                ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                Scopes = new List<Scope> {
                    new Scope("profile:read")
                }
            },
            new ApiResource {
                Name = "write-profile",
                DisplayName = "profile:write",
                Description = "Write access to user profile",
                UserClaims = new List<string> {"role"},
                ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                Scopes = new List<Scope> {
                    new Scope("profile:write")
                }
            }
        };
  }
  
}
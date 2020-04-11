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
              new IdentityResources.Address(),
              new IdentityResources.Phone(),
              new IdentityResource {
                  Name = "subject",
                  UserClaims = new List<string> {
                      OpenIdConnectConstants.Claims.Subject
                  }
              }
          };
  }
  
}
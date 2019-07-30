using System;
using System.Collections.Generic;
using IdentityServer4.Test;
using IdentityModel;
using System.Security.Claims;

namespace CounterCulture.Utilities { 
  public partial class DataSeed {
    public static List<TestUser> TestUsers => new List<TestUser> {
      new TestUser {
        SubjectId = Guid.NewGuid().ToString(),
        Username = "bruce",
        Password = "banner",
        Claims = new List<Claim> {
            new Claim(JwtClaimTypes.Email, "bruce.banner@example.com"),
            new Claim(JwtClaimTypes.Role, "admin")
        }
      },
      new TestUser {
        SubjectId = Guid.NewGuid().ToString(),
        Username = "clark",
        Password = "kent",
        Claims = new List<Claim> {
            new Claim(JwtClaimTypes.Email, "clark.kent@example.com"),
            new Claim(JwtClaimTypes.Role, "user")
        }
      },
      new TestUser {
        SubjectId = Guid.NewGuid().ToString(),
        Username = "peter",
        Password = "parker",
        Claims = new List<Claim> {
            new Claim(JwtClaimTypes.Email, "peter.parker@example.com"),
            new Claim(JwtClaimTypes.Role, "subscriber")
        }
      }
    };
  }
}
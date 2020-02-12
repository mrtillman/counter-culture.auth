using System;
using System.Collections.Generic;
using IdentityServer4.Test;
using IdentityModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Utilities { 
  public partial class DataSeed {
    public static List<IdentityUser> Users => new List<IdentityUser> {
            new IdentityUser(){
                UserName = "barry.allen@example.com",
                NormalizedUserName = "BARRY.ALLEN@EXAMPLE.COM",
                Email = "barry.allen@example.com",
                NormalizedEmail = "BARRY.ALLEN@EXAMPLE.COM"
            },
            new IdentityUser(){
                UserName = "bruce.banner@example.com",
                NormalizedUserName = "BRUCE.BANNER@EXAMPLE.COM",
                Email = "bruce.banner@example.com",
                NormalizedEmail = "BRUCE.BANNER@EXAMPLE.COM"
            },
            new IdentityUser(){
                UserName = "clark.kent@example.com",
                NormalizedUserName = "CLARK.KENT@EXAMPLE.COM",
                Email = "clark.kent@example.com",
                NormalizedEmail = "CLARK.KENT@EXAMPLE.COM"
            } //,
            // new IdentityUser(){
            //     UserName = "peter.parker@example.com",
            //     NormalizedUserName = "PETER.PARKER@EXAMPLE.COM",
            //     Email = "peter.parker@example.com",
            //     NormalizedEmail = "PETER.PARKER@EXAMPLE.COM"
            // },
            
    };

    public static Dictionary<string, string> UserPasswords => new Dictionary<string, string>{
        { DataSeed.Users[0].UserName, "WVPMHDma*kX6#JDV" },
        { DataSeed.Users[1].UserName, "4tVz%JZD8huTR%gc" },
        { DataSeed.Users[2].UserName, "$3U%rhI30%K1je02" },
        // { "peter.parker@example.com", "@QtFwZfF}3*y=/=j" }
    };
  }
}
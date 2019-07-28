using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using IdentityServer4.EntityFramework.DbContexts;
using CounterCulture.Utilities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class OnStartupFilter : IStartupFilter
{

    private readonly IServiceProvider _serviceProvider;
    public OnStartupFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        using(var scope = _serviceProvider.CreateScope()){

            var is4Context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            
            if(!is4Context.Clients.Any()){
                is4Context.Clients.AddRange(
                    DataSeed.Clients
                            .Select(client => client.ToEntity()));
            }

            if(!is4Context.IdentityResources.Any()){
                is4Context.IdentityResources.AddRange(
                    DataSeed.IdentityResources
                            .Select(resource => resource.ToEntity()));
            }

            if(!is4Context.ApiResources.Any()){
                is4Context.ApiResources.AddRange(
                    DataSeed.ApiResources
                            .Select(resource => resource.ToEntity()));
            }

            if(!userManager.Users.Any()){
                IdentityUsers.ForEach(user => {
                    var result = Task.Run(() => userManager.CreateAsync(user, UserPasswords[user.UserName])).Result;
                });
            }

            is4Context.SaveChanges();
        }

        return builder =>
        {
            builder.UseMiddleware<RequestServicesContainerMiddleware>();
            next(builder);
        };
    }

    public List<IdentityUser> IdentityUsers => new List<IdentityUser> {
        new IdentityUser(){
            UserName = "clark",
            NormalizedUserName = "CLARK",
            Email = "clark.kent@example.com",
            NormalizedEmail = "CLARK.KENT@EXAMPLE.COM"
        },
        new IdentityUser(){
            UserName = "bruce",
            NormalizedUserName = "BRUCE",
            Email = "bruce.banner@example.com",
            NormalizedEmail = "BRUCE.BANNER@EXAMPLE.COM"
        },
        new IdentityUser(){
            UserName = "peter",
            NormalizedUserName = "PETER",
            Email = "peter.parker@example.com",
            NormalizedEmail = "PETER.PARKER@EXAMPLE.COM"
        }
        // new IdentityUser(){
        //     UserName = "tom",
        //     NormalizedUserName = "TOM",
        //     Email = "tom.ford@example.com",
        //     NormalizedEmail = "TOM.FORD@EXAMPLE.COM"
        // }
        
    };

    public Dictionary<string, string> UserPasswords => new Dictionary<string, string>{
        {"clark","WVPMHDma*kX6#JDV"},
        {"bruce","4tVz%JZD8huTR%gc"},
        {"peter","$3U%rhI30%K1je02"},
        //{"tom","@QtFwZfF}3*y=/=j"}
    };

}

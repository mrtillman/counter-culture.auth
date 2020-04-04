using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using IdentityServer4.EntityFramework.DbContexts;
using Presentation.Utilities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Presentation.Configuration
{
  public class OnStartupFilter : IStartupFilter
  {

    private readonly IServiceProvider _serviceProvider;
    public OnStartupFilter(IServiceProvider serviceProvider,
                        IWebHostEnvironment hostingEnvironment)
    {
      _serviceProvider = serviceProvider;
      _env = hostingEnvironment;
    }

    public IWebHostEnvironment _env { get; set; }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
      using (var scope = _serviceProvider.CreateScope())
      {

        var is4Context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        DataSeed.ServerUrls = new ServerUrls(_env);

        if (!is4Context.Clients.Any())
        {
          is4Context.Clients.AddRange(
              DataSeed.Clients
                      .Select(client => client.ToEntity()));
        }

        if (!is4Context.IdentityResources.Any())
        {
          is4Context.IdentityResources.AddRange(
              DataSeed.IdentityResources
                      .Select(resource => resource.ToEntity()));
        }

        if (!is4Context.ApiResources.Any())
        {
          is4Context.ApiResources.AddRange(
              DataSeed.ApiResources
                      .Select(resource => resource.ToEntity()));
        }

        if (!userManager.Users.Any())
        {
          DataSeed.Users.ForEach(user =>
          {
            var result = Task.Run(() 
              => userManager.CreateAsync(user, 
                  DataSeed.UserPasswords[user.UserName])).Result;
          });
        }

        is4Context.SaveChanges();
      }

      return builder =>
      {
        //builder.UseMiddleware<RequestServicesContainerMiddleware>();
        next(builder);
      };
    }

  }

}
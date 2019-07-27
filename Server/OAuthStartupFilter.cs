using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using IdentityServer4.EntityFramework.DbContexts;
using CounterCulture.Utilities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.DependencyInjection;

public class OAuthStartupFilter : IStartupFilter
{

    private readonly IServiceProvider _serviceProvider;
    public OAuthStartupFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        using(var scope = _serviceProvider.CreateScope()){
            var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            // seed clients
            if(!context.Clients.Any()){
                context.Clients.AddRange(
                    DataSeed.Clients.Select(client => client.ToEntity()));
            }        

            // seed identity resources
            // seed api resources
            // seed users
            context.SaveChanges();
        }

        return builder =>
        {
            builder.UseMiddleware<RequestServicesContainerMiddleware>();
            next(builder);
        };
    }
}

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using CounterCulture.Models;
using CounterCulture.Repositories;
using CounterCulture.Services;

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

            var oauthRepo = scope.ServiceProvider.GetRequiredService<IOAuthRepository>();
            var oauthService = scope.ServiceProvider.GetRequiredService<IOAuthService>();

            if(oauthRepo.isEmpty){
                oauthService.RegisterClient(new OAuthClient() {
                    app_type = "web",
                    app_name = "counter-culture.app",
                    app_description = "counter app",
                    homepage_uri = "http://localhost:8080",
                    redirect_uri = "http://localhost:8080",
                    grant_types = "code",
                    scope = "read+write"
                });
                oauthService.RegisterClient(new OAuthClient() {
                    app_type = "web",
                    app_name = "counter-culture.dev",
                    app_description = "geek site",
                    homepage_uri = "http://localhost:9000",
                    redirect_uri = "http://localhost:9000",
                    grant_types = "code",
                    scope = "read+write"
                });
            }

            // TODO: seed user accounts

        }

        return builder =>
        {
            builder.UseMiddleware<RequestServicesContainerMiddleware>();
            next(builder);
        };
    }
}

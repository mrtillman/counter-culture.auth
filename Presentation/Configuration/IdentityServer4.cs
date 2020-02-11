using CounterCulture.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Common;

namespace CounterCulture.Configuration
{
  public static class IdentityServer4Configuration
  {

    private static string migrationsAssembly
    {
      get
      {
        return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
      }
    }
    
    public static void ConfigureIdentityServer4(
      this IServiceCollection services, string mySqlConnectionString, IServerUrls ServerUrls)
    {
      services.AddIdentityServer(options => 
                  options.PublicOrigin = ServerUrls.SECURE)
              .AddDeveloperSigningCredential()
              .AddOperationalStore(options =>
                  options.ConfigureDbContext = builder =>
                      builder.UseMySql(mySqlConnectionString, sqlOptions =>
                          sqlOptions.MigrationsAssembly(migrationsAssembly)))
              .AddConfigurationStore(options =>
                  options.ConfigureDbContext = builder =>
                      builder.UseMySql(mySqlConnectionString, sqlOptions =>
                          sqlOptions.MigrationsAssembly(migrationsAssembly)))
              .AddAspNetIdentity<IdentityUser>();
      
    }
  }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CounterCulture.Repositories;

namespace CounterCulture.Configuration
{
  public static class AspNetIdentityConfiguration
  {

    private static string migrationsAssembly
    {
      get
      {
        return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
      }
    }
    
    public static void ConfigureAspNetIdentity(this IServiceCollection services, string mySqlConnectionString)
    {
      services.AddDbContext<SecureDbContext>(options => 
        options.UseMySql(
                  mySqlConnectionString,
                  mySqlOptions => 
                    mySqlOptions.MigrationsAssembly(migrationsAssembly)));

      services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<SecureDbContext>()
              .AddDefaultTokenProviders();
    }
  }
}
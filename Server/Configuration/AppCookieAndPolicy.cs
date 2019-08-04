using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CounterCulture.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CounterCulture.Configuration
{
  public static class AppCookeAndPolicyConfiguration
  {
    public static void ConfigureAppCookieAndPolicy(this IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
            {
              options.CheckConsentNeeded = context => true;
              options.MinimumSameSitePolicy = SameSiteMode.None;
            });

      services.ConfigureApplicationCookie(options =>
      {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.LoginPath = "/account/login";
        options.AccessDeniedPath = "/";
        options.SlidingExpiration = true;
      });
    }
  }
}
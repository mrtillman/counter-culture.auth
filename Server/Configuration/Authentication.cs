using System;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CounterCulture.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using CounterCulture.Constants;

namespace CounterCulture.Configuration
{
  public static class AuthenticationConfiguration
  {
    public static void ConfigureAuthentication(
      this IServiceCollection services, string appSecret, ENV environment)
    {
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

      var signingKey = Encoding.ASCII
                      .GetBytes(appSecret);

      var _tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(signingKey),
        ValidateIssuer = true,
        ValidIssuer = ServerUrls.SECURE[environment],
        ValidateAudience = true,
        ValidAudience = $"{ServerUrls.SECURE[environment]}/resources"
      };

      services.AddAuthentication()
      .AddCookie(options => options.SlidingExpiration = true)
      .AddJwtBearer(options =>
      {
        options.Authority = ServerUrls.SECURE[environment];
        options.Audience = $"{ServerUrls.SECURE[environment]}/resources";
        options.RequireHttpsMetadata = (environment == ENV.PROD);
        options.SaveToken = true;
        options.TokenValidationParameters = _tokenValidationParameters;
      });
    }
  }
}
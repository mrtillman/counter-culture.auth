using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Presentation.Constants;
using Common;

namespace Presentation.Configuration
{
  public static class AuthenticationConfiguration
  {
    public static void ConfigureAuthentication(
      this IServiceCollection services, 
      IServerUrls ServerUrls,
      string appSecret, 
      ENV mode)
    {
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

      var signingKey = Encoding.ASCII
                      .GetBytes(appSecret);

      var _tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(signingKey),
        ValidateIssuer = true,
        ValidIssuer = ServerUrls.SECURE,
        ValidateAudience = false,
        ValidAudience = $"{ServerUrls.SECURE}/resources"
      };

      services.AddAuthentication()
      .AddCookie(options => options.SlidingExpiration = true)
      .AddJwtBearer(options =>
      {
        options.Authority = ServerUrls.SECURE;
        options.Audience = $"{ServerUrls.SECURE}/resources";
        options.RequireHttpsMetadata = (mode == ENV.PROD);
        options.SaveToken = true;
        options.TokenValidationParameters = _tokenValidationParameters;
      });
    }
  }
}
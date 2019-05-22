using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using CounterCulture.Models;

namespace CounterCulture.Utilities {

    public static class JWTAuthenticator {

        public static AuthResponse Authenticate(OAuthClient client, string secret){
            if(client == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var signingKey = Encoding.ASCII.GetBytes(secret);
            var tomorrow = DateTime.UtcNow.AddDays(1);
            var descriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim("client_id", client.client_id),
                    new Claim("user_id", client.user_id),
                    new Claim("scope", client.scope)
                }),
                Expires = tomorrow,
                SigningCredentials = new SigningCredentials(
                                     new SymmetricSecurityKey(signingKey), 
                                     SecurityAlgorithms.HmacSha256Signature)
            };
            var _token = tokenHandler.CreateToken(descriptor);
            var token = tokenHandler.WriteToken(_token);
            var expiresIn = (tomorrow - DateTime.UtcNow);
            return (new AuthResponse(){
                access_token = token,
                token_type = "bearer",
                expires_in = Math.Floor(expiresIn.TotalSeconds),
                expiration_date = descriptor.Expires
            });
        }
        
    }

}
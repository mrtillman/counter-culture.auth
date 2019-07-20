using System;
using System.Text;
using System.Collections.Generic;
using CounterCulture.Repositories;
using CounterCulture.Models;
using CounterCulture.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CounterCulture.Services {

    public class OAuthService : IOAuthService
    {
        public OAuthService(
            IConfiguration Configuration,
            IOAuthRepository OAuthRepository,
            ILogger<OAuthService> LoggerService)
        {
            OAuthRepo = OAuthRepository;
            Config = Configuration;
            Logger = LoggerService;
        }

        IOAuthRepository OAuthRepo { get; set; }
        IConfiguration Config { get; }

        ILogger<OAuthService> Logger { get; set; }
        
        public AuthResponse Authenticate(OAuthClient client){
            if(client == null) return null;
            var appSecret = Config["AppSecret"];
            //return JWTAuthenticator.Authenticate(client, appSecret);
            return null;
        }

        public OAuthClient GetClient(string client_id) {
            return OAuthRepo.Get(client_id);
        }

        public OAuthClient FindClient(
            string client_id, string client_secret, string redirect_uri)
        {
            if(String.IsNullOrWhiteSpace(client_id)
               || String.IsNullOrWhiteSpace(client_secret)
               || String.IsNullOrWhiteSpace(redirect_uri)) return null;
            var match = new OAuthClient(){
                client_id = client_id,
                client_secret = client_secret,
                redirect_uri = redirect_uri
            };
            return OAuthRepo.Find(match);
        }
        public OAuthClient RegisterClient(OAuthClient client) {
            var e = string.Empty;
            client.client_id = e.NewClientId();
            client.client_secret = e.NewClientSecret();
            if(OAuthRepo.Save(client)){
                return client;
            }
            return null;
        }

    }
}
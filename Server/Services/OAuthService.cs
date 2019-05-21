using System;
using System.Text;
using System.Collections.Generic;
using CounterCulture.Repositories;
using CounterCulture.Models;
using CounterCulture.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace CounterCulture.Services {

    public class OAuthService : IOAuthService
    {
        public OAuthService(
            IConfiguration Configuration,
            IOAuthRepository OAuthRepository)
        {
            OAuthRepo = OAuthRepository;
            Config = Configuration;
        }

        private IOAuthRepository OAuthRepo { get; set; }
        public IConfiguration Config { get; }
        
        public AuthResponse Authenticate(OAuthClient client){
            if(client == null) return null;
            var appSecret = Config.GetValue<string>("AppSecret");
            return JWTAuthenticator.Authenticate(client, appSecret);
        }

        public OAuthClient GetClient(string client_id) {
            return OAuthRepo.Get(client_id);
        }

        public OAuthClient RegisterClient(OAuthClient client) {
            client.client_id = _generateClientId();
            client.client_secret = _generateClientSecret();
            if(OAuthRepo.Save(client)){
                return client;
            }
            return null;
        }

        private string _generateClientId() {
            var clientId = Guid.NewGuid().ToString();
            return _hexEncode(clientId);
        }

        private string _generateClientSecret() {
            return SHA256Hash.Compute(_generateClientId());
        }

        private string _hexEncode(string input){
            var bytes = Encoding.Default.GetBytes(input);
            var hexString = BitConverter.ToString(bytes);
            return hexString.Replace("-","");
        }
    }
}
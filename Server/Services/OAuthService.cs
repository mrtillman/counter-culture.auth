using System;
using System.Text;
using System.Collections.Generic;
using CounterCulture.Repositories;
using CounterCulture.Repositories.Models;
using CounterCulture.Utilities;
using Microsoft.Extensions.Options;

namespace CounterCulture.Services {

    public class OAuthService : IOAuthService
    {
        public OAuthService(
            IOAuthRepository OAuthRepository, IOptions<AppSecrets> appSecrets)
        {
            OAuthRepo = OAuthRepository;
            _secrets = appSecrets.Value;
        }

        private IOAuthRepository OAuthRepo { get; set; }
        private readonly AppSecrets _secrets;
        
        public AuthResponse Authenticate(OAuthClient client){
            if(client == null) return null;
            return JWTAuthenticator.Authenticate(client, _secrets.Secret);
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
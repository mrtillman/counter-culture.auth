using System;
using System.Text;
using System.Collections.Generic;
using CounterCulture.Repositories;
using CounterCulture.Repositories.Models;
using CounterCulture.Utilities;

namespace CounterCulture.Services {
    public class OAuthService : IOAuthService
    {
        public OAuthService(
            AppSecrets _appSecrets,
            IOAuthRepository OAuthRepository){
            appSecrets = _appSecrets;
            OAuthRepo = OAuthRepository;
        }

        private AppSecrets appSecrets { get; set; }
        private IOAuthRepository OAuthRepo { get; set; }
        
        public AuthResponse Authenticate(OAuthClient client){
            if(client == null) return null;
            return JWTAuthenticator.Authenticate(client, appSecrets.Secret);
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
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using CounterCulture.Repositories;
using CounterCulture.Repositories.Models;
using CounterCulture.Helpers;

namespace CounterCulture.Services {
    public class OAuthService : IOAuthService
    {
        public OAuthService(IOAuthRepository OAuthRepository){
            OAuthRepo = OAuthRepository;
        }

        public IOAuthRepository OAuthRepo { get; set; }

        public OAuthClient RegisterClient(OAuthClient client) {
            client.client_id = _generateClientId();
            client.client_secret = _generateClientSecret();
            if(OAuthRepo.SaveOAuthClient(client)){
                return client;
            }
            return null;
        }

        public OAuthClient GetClient(string clientId) {
            return OAuthRepo.GetOAuthClient(clientId);
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
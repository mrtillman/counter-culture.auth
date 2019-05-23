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
            var appSecret = Environment.GetEnvironmentVariable("AppSecret");
            return JWTAuthenticator.Authenticate(client, appSecret);
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
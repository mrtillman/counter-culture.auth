using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CounterCulture.Repositories;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Services {
    public class OAuthService : IOAuthService
    {
        public OAuthService(IOAuthRepository OAuthRepository){
            OAuthRepo = OAuthRepository;
        }

        public IOAuthRepository OAuthRepo { get; set; }

        public OAuthClient RegisterClient(OAuthClient client) {
            client.client_id = "generate client_id";
            client.client_secret = "generate client_secret";
            if(OAuthRepo.SaveOAuthClient(client)){
                return client;
            }
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using repositories;
using repositories.models;
using System.Linq;
using Microsoft.Extensions.Options;
using services.helpers;
using MySql.Data.MySqlClient;

namespace services
{
    public class UserService : IUserService
    {
        public UserService(IUserRepository UserRepository, IOptions<AppSecrets> appSecrets)
        {
            UserRepo = UserRepository;
            _secrets = appSecrets.Value;
        }
        
        public IUserRepository UserRepo { get; set; }
        private readonly AppSecrets _secrets;

        // [MySqlTimeoutExceptionFilter]
        public User Find(string username, string password)
        {
            if(String.IsNullOrEmpty(username) || 
               String.IsNullOrEmpty(password)) return null;
            
            // return await UserRepo.Find(username, password);
            
            User user = null;

            try
            {
                user = UserRepo.Find(username, password);
            } catch (System.IO.EndOfStreamException ex) {
		        Console.WriteLine(ex);
            } catch (System.IO.IOException ex){
		        Console.WriteLine(ex);
            } catch (MySqlException ex){
                Console.WriteLine(ex);
            }

            if(UserRepo.IsDisconnected){
                Console.WriteLine("trying reconnect...");
                Thread.Sleep(1000);
                return UserRepo.Reconnect()
                               .Find(username, password);
            }

            return user;
        }

        public async Task<bool> Create(string username, string password){
            return await UserRepo.Create(username, password);
        }

        public async Task<bool> Exists(string username){
            return await UserRepo.Exists(username);
        }

        public AuthResponse Authenticate(User user){
            if(user == null) return null;
            return JWTAuthenticator.Authenticate(user, _secrets.Secret);
        }
    }
}

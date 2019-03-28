using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using CounterCulture.Repositories;
using CounterCulture.Repositories.Models;
using CounterCulture.Auth.Helpers;

namespace CounterCulture.Services
{
    public class UserService : IUserService
    {
        public UserService(IUserRepository UserRepository, IOptions<AppSecrets> appSecrets)
        {
            UserRepo = UserRepoProxy<IUserRepository>.Create(UserRepository);
            _secrets = appSecrets.Value;
        }
        
        public IUserRepository UserRepo { get; set; }
        private readonly AppSecrets _secrets;

        public User Find(string username, string password)
        {
            if(String.IsNullOrEmpty(username) || 
               String.IsNullOrEmpty(password)) return null;
            
            User user = null;
            
            handleIOException(() => {
                user = UserRepo.Find(username, password);
            });

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

        private void handleIOException(Action action){
            try
            {
                action();
            } catch (System.IO.EndOfStreamException ex) {
                throw new RepositoryIOException(ex.Message, ex);
            } catch (System.IO.IOException ex){
                throw new RepositoryIOException(ex.Message, ex);
            } catch (MySqlException ex){ 
                throw new RepositoryIOException(ex.Message, ex);
            }
        }
    }
}

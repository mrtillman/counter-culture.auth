using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using CounterCulture.Repositories;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Services
{
    public class UserService : IUserService
    {
        public UserService(
            IUserRepository UserRepository, IOptions<AppSecrets> appSecrets)
        {
            UserRepo = UserRepository;
            _secrets = appSecrets.Value;
        }
        
        public IUserRepository UserRepo { get; set; }
        private readonly AppSecrets _secrets;

        public User Find(string username, string password)
        {
            if(String.IsNullOrEmpty(username) || 
               String.IsNullOrEmpty(password)) return null;
            return UserRepo.Find(username, password);
        }

        public bool Create(string username, string password){
            return UserRepo.Create(username, password);
        }

        public bool Exists(string username){
            return UserRepo.Exists(username);
        }        

    }
}

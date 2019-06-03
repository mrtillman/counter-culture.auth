using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using CounterCulture.Repositories;
using CounterCulture.Models;

namespace CounterCulture.Services
{
    public class UserService : IUserService
    {
        public UserService(
            IUserRepository UserRepository)
        {
            UserRepo = UserRepository;
        }
        
        public IUserRepository UserRepo { get; set; }

        public User Find(string username, string password)
        {
            if(String.IsNullOrWhiteSpace(username) || 
               String.IsNullOrWhiteSpace(password)) return null;
            
            return UserRepo.Find(username, password);
        }

        public UserProfile FindById(int userId){
            return UserRepo.FindById(userId);
        }

        public bool Create(string username, string password){
            return UserRepo.Create(username, password);
        }

        public bool Exists(string username){
            return UserRepo.Exists(username);
        }        

    }
}

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
            IUserRepository UserRepository, 
            SecureDbContext context)
        {
            UserRepo = UserRepository;
            _context = context;
        }
        
        public IUserRepository UserRepo { get; set; }
        private readonly SecureDbContext _context;

        public User Find(string username, string password)
        {
            if(String.IsNullOrEmpty(username) || 
               String.IsNullOrEmpty(password)) return null;
            
            return _context.Users
                           .Where(user => user.Username == username)
                           .FirstOrDefault();
        }

        public User FindById(int userId){
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

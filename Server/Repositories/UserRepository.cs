using System;
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using CounterCulture.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace CounterCulture.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(
            SecureDbContext _context,
            ILogger<UserRepository> LoggerService){
                this.context = _context;
                Logger = LoggerService;
            }
        public bool IsDisconnected { get; set; }
        private readonly SecureDbContext context;
        ILogger<UserRepository> Logger { get; set; }
        
        public User Find(string username, string password)
        {
          return context.Users
                         .FirstOrDefault(user => 
                               user.Username == username 
                               && user.Password == password);
        }

        public UserProfile FindById(int userId)
        {
            return context.UserProfiles.FirstOrDefault(profile => profile.UserID == userId);
        }

        public bool Exists(string username){
            return context.Users.Where(user => user.Username == username).Count() == 1;
        }

        public bool Create(string username, string password)
        {
            context.Users.Add(new User(){ 
              Username = username,
              Password = password
            });
            return context.SaveChanges() == 1;
        }

    }
}

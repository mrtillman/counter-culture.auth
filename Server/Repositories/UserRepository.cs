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

// TODO: remove 
// (Replace with Microsoft.AspNetCore.Identity.UserManager)

namespace CounterCulture.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(
            SecureDbContext _context,
            ILogger<UserRepository> LoggerService)
            :base(_context){
                Logger = LoggerService;
            }
        
        ILogger<UserRepository> Logger { get; set; }
        
        public User Find(string username, string password)
        {
          return context.Users
                        .FirstOrDefault(user => 
                          (user.Username == username 
                              && user.Password == password));
        }

        public UserProfile FindById(int userId)
        {
            return context.UserProfiles
                          .FirstOrDefault(profile => 
                            profile.UserID == userId);
        }

        public bool Exists(string username){
            return context.Users
                          .FirstOrDefault(user => 
                            user.Username == username) != null;
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

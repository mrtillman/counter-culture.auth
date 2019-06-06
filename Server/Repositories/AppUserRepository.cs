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
    public class AppUserRepository : BaseRepository, IAppUserRepository
    {
        public AppUserRepository(
            SecureDbContext _context,
            ILogger<AppUserRepository> LoggerService)
            :base(_context){
                Logger = LoggerService;
        }
        
        ILogger<AppUserRepository> Logger { get; set; }

        public static List<AppUser> Users { get; set; } = new List<AppUser>();

        public bool AddUser(AppUser user) 
        {
            user.Id = Guid.NewGuid().ToString();
            user.Password = null;
            Users.Add(user);
            return true;
        }

        public AppUser FindUser(AppUser User)
        {
          return Users.FirstOrDefault(user => 
            user.Id == User.Id
            || user.UserName == User.UserName);
        }

        public AppUser FindByUserName(string UserName)
        {
          return Users.FirstOrDefault(user => 
            user.UserName == UserName);
        }

        public AppUser FindByID(string UserId)
        {
          return Users.FirstOrDefault(user => 
            user.Id == UserId);
        }

        public AppUser FindByEmail(string Email)
        {
          return Users.FirstOrDefault(user => user.Email == Email);
        }
      }
}

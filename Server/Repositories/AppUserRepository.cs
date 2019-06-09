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
        public bool AddUser(AppUser user) 
        {
            user.Password = null;
            context.Users.Add(user);
            return context.SaveChanges() == 1;
        }

        public AppUser FindUser(AppUser User)
        {
          return context.Users.FirstOrDefault(user => 
            user.Id == User.Id
            || user.UserName == User.UserName);
        }

        public AppUser FindByUserName(string UserName)
        {
          return context.Users.FirstOrDefault(user => 
            user.UserName == UserName);
        }

        public AppUser FindByID(string UserId)
        {
          return context.Users.FirstOrDefault(user => 
            user.Id == UserId);
        }

        public AppUser FindByEmail(string normalizedEmail)
        {
          return context.Users.FirstOrDefault(user => user.NormalizedEmail == normalizedEmail);
        }
      }
}

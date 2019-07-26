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
using IdentityServer4.Test;
using IdentityServer4.Quickstart.UI;

namespace CounterCulture.Repositories
{
    public class TestUserRepository : ITestUserRepository
    {

        private static List<TestUser> _testUsers;
        public TestUserRepository(
            ILogger<TestUser> LoggerService){
                Logger = LoggerService;
                _testUsers = TestUsers.Users;
        }
        
        ILogger<TestUser> Logger { get; set; }

        public bool AddUser(TestUser user) 
        {
            _testUsers.Add(user);
            return true;
        }

        public TestUser FindUser(TestUser User)
        {
          return _testUsers.FirstOrDefault(user => 
            user.SubjectId == User.SubjectId
            || user.Username == User.Username);
        }

        public TestUser FindByUserName(string Username)
        {
          return _testUsers.FirstOrDefault(user => 
            user.Username == Username);
        }

        public TestUser FindByID(string UserId)
        {
          return _testUsers.FirstOrDefault(user => 
            user.SubjectId == UserId);
        }

        public TestUser FindByEmail(string normalizedEmail)
        {
          return _testUsers.FirstOrDefault(user => 
            String.Equals(user.Username, normalizedEmail, 
              StringComparison.OrdinalIgnoreCase));
        }
      }
}

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using repositories.models;

namespace services {
    public interface IUserService
    {
        bool Exists(string username);
        Task<User> Find(string username, string password);
        bool Create(string username, string password);
        string Authenticate(User user);
    }
}
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using repositories.models;

namespace services {
    public interface IUserService
    {
        Task<bool> Exists(string username);
        Task<User> Find(string username, string password);
        Task<bool> Create(string username, string password);
        string Authenticate(User user);
    }
}
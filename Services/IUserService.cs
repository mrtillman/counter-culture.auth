using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Services {
    public interface IUserService
    {
        Task<bool> Exists(string username);
        User Find(string username, string password);
        Task<bool> Create(string username, string password);
        AuthResponse Authenticate(User user);
    }
}
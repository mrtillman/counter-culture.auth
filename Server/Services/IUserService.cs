using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Services {
    public interface IUserService
    {
        bool Exists(string username);
        User Find(string username, string password);
        bool Create(string username, string password);
    }
}
using System;
using System.Collections.Generic;
using CounterCulture.Models;

namespace CounterCulture.Services {
    public interface IUserService
    {
        bool Exists(string username);
        User Find(string username, string password);
        UserProfile FindById(int userId);
        bool Create(string username, string password);
    }
}
using System;
using System.Collections.Generic;
using CounterCulture.Models;

namespace CounterCulture.Services {
    public interface IUserService
    {
        bool Exists(string username);
        UserForm Find(string username, string password);
        User FindById(int userId);
        bool Create(string username, string password);
    }
}
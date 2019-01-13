using System;
using System.Collections.Generic;
using repositories.models;

namespace services {
    public interface IUserService
    {
        bool Exists(string username);
        User Find(string username, string password);
        bool Create(string username, string password);
        string Authenticate(User user);
    }
}
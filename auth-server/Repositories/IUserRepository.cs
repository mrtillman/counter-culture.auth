using System;
using System.Collections.Generic;
using repositories.models;

namespace repositories
{
    public interface IUserRepository
    {
        bool Exists(string username);
        User Find(string username, string password);
        bool Create(string username, string password);
    }
}

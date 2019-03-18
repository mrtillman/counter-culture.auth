using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using repositories.models;

namespace repositories
{
    public interface IUserRepository
    {
        bool Exists(string username);
        Task<User> Find(string username, string password);
        bool Create(string username, string password);
    }
}

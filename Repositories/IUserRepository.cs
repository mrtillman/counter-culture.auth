using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using repositories.models;

namespace repositories
{
    public interface IUserRepository
    {
        Task<bool> Exists(string username);
        Task<User> Find(string username, string password);
        Task<bool> Create(string username, string password);
    }
}

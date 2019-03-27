using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using repositories.models;

namespace repositories
{
    public interface IUserRepository
    {
        bool IsDisconnected { get; }
        Task<bool> Exists(string username);
        User Find(string username, string password);
        Task<bool> Create(string username, string password);
        IUserRepository Reconnect();
    }
}

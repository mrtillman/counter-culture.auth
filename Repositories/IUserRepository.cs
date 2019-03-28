using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Repositories
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

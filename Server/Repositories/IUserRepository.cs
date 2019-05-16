using System;
using System.Collections.Generic;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Repositories
{
    public interface IUserRepository
    {
        bool IsDisconnected { get; }
        bool Exists(string username);
        User Find(string username, string password);
        bool Create(string username, string password);
        IUserRepository Reconnect();
    }
}

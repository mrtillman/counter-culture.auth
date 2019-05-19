using System;
using System.Collections.Generic;
using CounterCulture.Models;

namespace CounterCulture.Repositories
{
    public interface IUserRepository
    {
        bool IsDisconnected { get; }
        bool Exists(string username);
        User Find(string username, string password);
        User FindById(int userId);
        bool Create(string username, string password);
        IUserRepository Reconnect();
    }
}

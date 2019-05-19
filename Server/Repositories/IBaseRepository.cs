using System;
using System.Collections.Generic;
using CounterCulture.Models;

namespace CounterCulture.Repositories
{
    public interface IBaseRepository 
    {
        bool IsDisconnected { get; }
        object Reconnect();
    }

    public interface IBaseRepository<T>
    {
        bool IsDisconnected { get; }
        T Reconnect();
    }
}

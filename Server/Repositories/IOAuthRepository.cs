using System;
using System.Collections.Generic;
using CounterCulture.Models;

namespace CounterCulture.Repositories
{
    public interface IOAuthRepository
    {
        bool Save(OAuthClient client);
        OAuthClient Get(string client_id);
        OAuthClient Find(string client_id, string secret);
    }
}

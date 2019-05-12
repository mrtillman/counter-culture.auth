using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Services {
    public interface IOAuthService
    {
        OAuthClient RegisterClient(OAuthClient client);
    }
}
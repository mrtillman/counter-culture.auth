using System;

namespace CounterCulture.Constants
{
    public static class LoggingEvents
    {
        public static readonly string PersistOAuthClient = $"{LoggingActions.Persist}:oauth_client";
        public static readonly string RegisterApp = $"{LoggingActions.Register}:app";
    }
}
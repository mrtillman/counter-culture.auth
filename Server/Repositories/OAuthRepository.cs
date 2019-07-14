using CounterCulture.Models;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace CounterCulture.Repositories
{
  public class OAuthRepository : BaseRepository, IOAuthRepository
    {
        public OAuthRepository(
            SecureDbContext _context,
            ILogger<OAuthRepository> LoggerService)
            :base(_context) { 
                Logger = LoggerService; 
            }

        private ILogger<OAuthRepository> Logger { get; set; }

        public bool isEmpty {
            get {
                return context.OAuthClients.Count() == 0;
            }
        }

        public bool Save(OAuthClient client)
        {
            context.OAuthClients.Add(client);
            return context.SaveChanges() == 1;
        }

        public OAuthClient Get(string client_id) {
            return context.OAuthClients
                          .FirstOrDefault(client => 
                            client.client_id == client_id);
        }

        public OAuthClient Find(OAuthClient client) {
            return context.OAuthClients
                          .FirstOrDefault(_client =>
                            _client.client_id == client.client_id &&
                            _client.client_secret == client.client_secret &&
                            _client.redirect_uri == client.redirect_uri);
        }

        public OAuthClient Find(string client_id, string client_secret) {
            return context.OAuthClients
                          .FirstOrDefault(client =>
                            client.client_id == client_id &&
                            client.client_secret == client_secret);
        }

    }
}

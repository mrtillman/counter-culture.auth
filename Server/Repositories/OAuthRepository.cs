using System;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Repositories
{
    public class OAuthRepository : IOAuthRepository
    {
        public OAuthRepository(MySqlConnection _connection, AppSecrets _appSecrets)
        {
            connection = _connection;
            if(connection.State == ConnectionState.Closed){
                connection.Open();
            }
            appSecrets = _appSecrets;
        }

        public bool IsDisconnected {
            get {
                return connection.State == ConnectionState.Closed;
            }
        }

        private MySqlConnection connection { get; set; }
        private AppSecrets appSecrets { get; set; }

        public bool SaveOAuthClient(OAuthClient client)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO `oauth_clients` ");
            builder.Append("(app_name, app_description, client_id, client_secret, redirect_uri, grant_types, scope, user_id) ");
            builder.Append($"VALUES ('{client.app_name}', '{client.app_description}', '{client.client_id}', '{client.client_secret}', '{client.redirect_uri}', '{client.grant_types}', '{client.scope}', '{client.user_id}');");
            var cmdText = builder.ToString();
            var command = new MySqlCommand(cmdText, connection);
            command.CommandType = CommandType.Text;
            return command.ExecuteNonQuery() == 1;
        }

    }
}

﻿using System;
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
            builder.Append("(app_type, app_name, app_description, client_id, client_secret, redirect_uri, homepage_uri, grant_types, scope, user_id) ");
            builder.Append($"VALUES ('{client.app_type}', '{client.app_name}', '{client.app_description}', '{client.client_id}', '{client.client_secret}', '{client.redirect_uri}', '{client.homepage_uri}', '{client.grant_types}', '{client.scope}', '{client.user_id}');");
            var cmdText = builder.ToString();
            var command = new MySqlCommand(cmdText, connection);
            command.CommandType = CommandType.Text;
            return command.ExecuteNonQuery() == 1;
        }

        public OAuthClient GetOAuthClient(string clientId) {
            var client = new OAuthClient();
            var builder = new StringBuilder();
            builder.Append("SELECT app_type, app_name, app_description, client_id, client_secret, redirect_uri, homepage_uri, grant_types, scope, user_id FROM `oauth_clients` ");
            builder.Append($"WHERE client_id = '{clientId}';");
            var cmdText = builder.ToString();
            var command = new MySqlCommand(cmdText, connection);
            command.CommandType = CommandType.Text;
            using(DbDataReader rdr = command.ExecuteReader()){
                if(!rdr.HasRows) return null;
                rdr.Read();
                client.app_type = rdr.GetFieldValue<string>(0);
                client.app_name = rdr.GetFieldValue<string>(1);
                client.app_description = rdr.GetFieldValue<string>(2);
                client.client_id = rdr.GetFieldValue<string>(3);
                client.client_secret = rdr.GetFieldValue<string>(4);
                client.redirect_uri = rdr.GetFieldValue<string>(5);
                client.homepage_uri = rdr.GetFieldValue<string>(6);
                client.grant_types = rdr.GetFieldValue<string>(7);
                client.scope = rdr.GetFieldValue<string>(8);
                client.user_id = rdr.GetFieldValue<string>(9);
                rdr.Close();
            }
            return client;
        }

    }
}

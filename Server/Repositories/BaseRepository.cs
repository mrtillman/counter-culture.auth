using System;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using CounterCulture.Models;

namespace CounterCulture.Repositories
{
    public class BaseRepository
    {
        public BaseRepository(MySqlConnection _connection, AppSecrets _appSecrets)
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

        protected MySqlConnection connection { get; set; }
        protected AppSecrets appSecrets { get; set; }
    }
}
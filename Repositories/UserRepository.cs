using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using repositories.models;

namespace repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(MySqlConnection _connection)
        {
            connection = _connection;
            _checkConnection();
        }

        private MySqlConnection connection { get; set; }

        public async Task<User> Find(string username, string password)
        {
            await _checkConnectionAsync();
            User user = new User();
            var query = $"SELECT * FROM `Users` WHERE Username = '{username}' AND Password = '{password}'";
            var command = new MySqlCommand(query, connection);
            command.CommandType = CommandType.Text;
            using(DbDataReader rdr = await command.ExecuteReaderAsync()){
                if(!rdr.HasRows) return null;
                rdr.Read();
                user.ID = rdr.GetFieldValue<int>(0);
                user.Username = rdr.GetFieldValue<string>(1);
                rdr.Close();
            }
            return user;
        }

        public bool Exists(string username){
            _checkConnection();
            var query = $"SELECT COUNT(ID) FROM `Users` WHERE Username = '{username}'";
            var command = new MySqlCommand(query, connection);
            bool exists = false;
            command.CommandType = CommandType.Text;
            using(MySqlDataReader rdr = command.ExecuteReader()){
                rdr.Read();
                exists = rdr.GetFieldValue<long>(0) > 0;
            }
            return exists;
        }

        public bool Create(string username, string password)
        {
            _checkConnection();
            var cmdText = $"INSERT INTO `Users` (Username, Password) VALUES ('{username}','{password}')";
            var command = new MySqlCommand(cmdText, connection);
            command.CommandType = CommandType.Text;
            return command.ExecuteNonQuery() == 1;
        }

        private void _checkConnection() {
            if(connection.State == ConnectionState.Closed){
                connection.Open();
            }
        }

        private async Task _checkConnectionAsync() {
            if(connection.State == ConnectionState.Closed){
                await Task.Run(() => connection.OpenAsync());
            }
        }
    }
}

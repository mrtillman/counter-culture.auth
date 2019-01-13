using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using repositories.models;

namespace repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(MySqlConnection _connection)
        {
            connection = _connection;
        }

        private MySqlConnection connection { get; set; }

        public User Find(string username, string password)
        {
            User user = new User();
            var query = $"SELECT * FROM `Users` WHERE Username = '{username}' AND Password = '{password}'";
            var command = new MySqlCommand(query, connection);
            command.CommandType = CommandType.Text;
            using(MySqlDataReader rdr = command.ExecuteReader()){
                if(!rdr.HasRows) return null;
                rdr.Read();
                user.ID = rdr.GetFieldValue<int>(0);
                user.Username = rdr.GetFieldValue<string>(1);
                rdr.Close();
            }
            return user;
        }

        public bool Exists(string username){
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
            var cmdText = $"INSERT INTO `Users` (Username, Password) VALUES ('{username}','{password}')";
            var command = new MySqlCommand(cmdText, connection);
            command.CommandType = CommandType.Text;
            return command.ExecuteNonQuery() == 1;
        }
    }
}

using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Repositories
{
    public class UserRepository : BaseRepository, IBaseRepository, IUserRepository
    {
        public UserRepository(
            MySqlConnection _connection, AppSecrets _appSecrets)
            :base(_connection, _appSecrets){ }

        public User Find(string username, string password)
        {
            User user = new User();
            var query = $"SELECT * FROM `Users` WHERE Username = '{username}' AND Password = '{password}'";
            var command = new MySqlCommand(query, connection);
            command.CommandType = CommandType.Text;
            
            using(DbDataReader rdr = command.ExecuteReader()){
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
            using(DbDataReader rdr = command.ExecuteReader()){
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

        public IUserRepository Reconnect(){
            connection = new MySqlConnection(appSecrets.MySQLConnectionString);
            connection.Open();
            return this;
        }

        object IBaseRepository.Reconnect(){
            return Reconnect();
        }

    }
}

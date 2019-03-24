﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using repositories;
using repositories.models;
using System.Linq;
using Microsoft.Extensions.Options;
using services.helpers;
using MySql.Data.MySqlClient;

namespace services
{
    public class UserService : IUserService
    {
        public UserService(IUserRepository UserRepository, IOptions<AppSecrets> appSecrets)
        {
            UserRepo = UserRepository;
            _secrets = appSecrets.Value;
        }
        
        public IUserRepository UserRepo { get; set; }
        private readonly AppSecrets _secrets;

        public async Task<User> Find(string username, string password)
        {
            if(String.IsNullOrEmpty(username) || 
               String.IsNullOrEmpty(password)) return null;

            try
            {
                return await UserRepo.Find(username, password);    
            }
            catch (MySqlException ex)
            {
                return await UserRepo.Reconnect()
                                     .Find(username, password);
            }
            
        }

        public async Task<bool> Create(string username, string password){
            return await UserRepo.Create(username, password);
        }

        public async Task<bool> Exists(string username){
            return await UserRepo.Exists(username);
        }

        public string Authenticate(User user){
            if(user == null) return null;
            return JWTAuthenticator.Authenticate(user, _secrets.Secret);
        }
    }
}
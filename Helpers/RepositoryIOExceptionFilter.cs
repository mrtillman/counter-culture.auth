using System;
using repositories;
using repositories.models;
using authentication_server.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace authentication_server.Helpers
{
    public class RepositoryIOExceptionFilter : IExceptionFilter
    {
        public void OnException (ExceptionContext context) {
/*
    if(UserRepo.IsDisconnected){
        Thread.Sleep(1000);
        return UserRepo.Reconnect()
                        .Find(username, password);
    }
*/
        }
    }
}
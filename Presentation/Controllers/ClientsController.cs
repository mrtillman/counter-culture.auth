﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Presentation.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation.Models;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4;
using Presentation;

namespace Presentation.Controllers
{

  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientsController : BaseController
    {
        public ClientsController(
            ILogger<ClientsController> LoggerService,
            ConfigurationDbContext _is4Context)
        {
            Logger = LoggerService;
            is4Context = _is4Context;
        }
        private ILogger<ClientsController> Logger { get; set; }

        private ConfigurationDbContext is4Context { get; set; }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public ActionResult Register([FromBody] Registration registration) {

            var client_secret = String.Empty.NewClientSecret();

            // TODO: simplify using Automapper
            Client client = new Client();
            client.AccessTokenType = AccessTokenType.Jwt;
            client.AccessTokenLifetime = 86400;
            client.ClientId = String.Empty.NewClientId();
            client.ClientName = registration.ClientName;
            client.AllowedGrantTypes = GrantTypes.CodeAndClientCredentials;
            client.AllowedScopes = DataSeed._allowedScopes;
            client.AllowOfflineAccess = true;
            client.RedirectUris = registration.RedirectUris;
            client.ClientSecrets = new List<Secret> {
                new Secret(client_secret.Sha256())
            };
            
            this.is4Context.Clients.Add(client.ToEntity());

            this.is4Context.SaveChanges();

            Logger.LogInformation("register:app", client.ClientName);

            client.ClientSecrets = new List<Secret> {	
                new Secret(client_secret)	
            };
            
            return Ok(client);
        }

    }
}

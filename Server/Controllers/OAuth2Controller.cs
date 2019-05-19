using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using CounterCulture.Services;
using CounterCulture.Constants;
using CounterCulture.Models;
using CounterCulture.Utilities;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace CounterCulture.Controllers
{
    
    [Authorize]
    public class OAuth2Controller : BaseController
    {
        public OAuth2Controller(
            ICacheService CacheService,
            ILogger<OAuth2Controller> LoggerService,
            IOAuthService OAuthService)
            :base(CacheService)
        {
            OAuth = OAuthService;
            Logger = LoggerService;
        }

        private IOAuthService OAuth { get; set; }
        private ILogger<OAuth2Controller> Logger { get; set; }

        [HttpGet]
        public ActionResult<OAuthClient> Get() {
            if (!User.Identity.IsAuthenticated){
                return Unauthorized();
            }
            var claim = User.Claims.ElementAt(0);
            var client_id = claim.Value;
            return Ok(OAuth.GetClient(client_id));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public ActionResult Register([FromBody] OAuthClient client) {
            Logger.LogInformation(LoggingEvents.RegisterApp, client.app_name);
            return Ok(OAuth.RegisterClient(client));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("access_token")]
        public ActionResult AccessToken(string authorization_code){
            if(String.IsNullOrEmpty(authorization_code)){
                return Unauthorized();
            }
            var auth = Cache.Get(authorization_code);
            if(String.IsNullOrEmpty(auth)){
                return Unauthorized();
            }
            Cache.Delete(authorization_code);
            var auth_parts = auth.Split(':');
            var client_id = auth_parts[0];
            var user_id = auth_parts[1];
            var client = OAuth.GetClient(client_id);
            if(client == null){
                return Unauthorized();
            }
            client.user_id = user_id;
            var authResponse = OAuth.Authenticate(client);
            if(authResponse == null){
                return Unauthorized();
            }
            return Ok(authResponse);
        }

    }
}

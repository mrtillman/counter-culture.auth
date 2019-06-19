using System;
using System.Web;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CounterCulture.Controllers
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [AllowAnonymous] // TODO: remove - require geek site token
        [Route("register")]
        public ActionResult Register([FromBody] OAuthClient client) {
            Logger.LogInformation(LoggingEvents.RegisterApp, client.app_name);
            return Ok(OAuth.RegisterClient(client));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("access_token")]
        public ActionResult AccessToken([FromQuery] AuthRequest authReq)
        {

            // TODO: check authReq.grant_type

            if(String.IsNullOrWhiteSpace(authReq.code)){
                return Unauthorized();
            }
            var authCacheValue = Cache.Get(authReq.code);
            if(String.IsNullOrWhiteSpace(authCacheValue)){
                return Unauthorized();
            }

            Cache.Delete(authReq.code);
            
            // TODO: simplify using string extensions
            var authParts = authCacheValue.Split(':');
            var clientId = authParts[0];
            var userId = authParts[1];
            var redirect_uri = HttpUtility.UrlDecode(authReq.redirect_uri);

            var client = OAuth.FindClient(
                            clientId, 
                            authReq.client_secret, 
                            redirect_uri);
            if(client == null){
                return Unauthorized();
            }
            client.user_id = userId;
            var authResponse = OAuth.Authenticate(client);
            if(authResponse == null){
                return Unauthorized();
            }
            return Ok(authResponse);
        }

    }
}

using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using CounterCulture.Services;
using CounterCulture.Repositories.Models;
using CounterCulture.Secure.Helpers;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace CounterCulture.Secure.Controllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class OAuthController : ControllerBase
    {
        public OAuthController(
            IOAuthService OAuthService, 
            IHostingEnvironment hostingEnvironment,
            ICacheService CacheService)
        {
            OAuth = OAuthService;
            env = hostingEnvironment;
            Cache = CacheService;
        }

        private readonly IOAuthService OAuth;
        private readonly IHostingEnvironment env;
        private readonly ICacheService Cache;

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public ActionResult Register([FromBody] OAuthClient client) {
            return Ok(OAuth.RegisterClient(client));
        }

        // [HttpPost]
        // [AllowAnonymous]
        // [Route("access_token")]
        // public ActionResult AccessToken([FromBody] Credentials credentials){
        //     return _authenticate(credentials.Username, credentials.Password);
        // }

        // [HttpPost]
        // [AllowAnonymous]
        // [Route("login")]
        // public ActionResult Login([FromForm] UserForm userForm) {
        //     return _authenticate(userForm.Username, userForm.Password, true);
        // }

        // private ActionResult _authenticate(string Username, string Password, bool performRedirect = false){
        //     string hashedPassword = SHA256Hash.Compute(Password);
        //     var user = Users.Find(Username, hashedPassword);
        //     AuthResponse authResponse = Users.Authenticate(user);
            
        //     if(authResponse == null) {
        //       return Unauthorized("Invalid username or password");
        //     }
            
        //     if(performRedirect) {
        //       var redirectOrigin = "https://www.counter-culture.io";
        //       if(env.IsDevelopment()){
        //           redirectOrigin = "http://localhost:8080";
        //       }
        //       return Redirect($"{redirectOrigin}/#token={authResponse.access_token}");
        //     }

        //     return Ok(authResponse);
        // }

    }
}

using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using CounterCulture.Services;
using CounterCulture.Repositories.Models;
using CounterCulture.Utilities;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace CounterCulture.Controllers
{
    
    [Authorize]
    public class OAuthController : BaseController
    {
        public OAuthController(
            IAuthService AuthService,
            ICacheService CacheService,
            IHostingEnvironment hostingEnvironment,
            IOAuthService OAuthService
            )
        {
            Auth = AuthService;
            Cache = CacheService;
            env = hostingEnvironment;
            OAuth = OAuthService;
        }

        private readonly IAuthService Auth;
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

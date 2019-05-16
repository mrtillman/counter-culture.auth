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
            ICacheService CacheService,
            IOAuthService OAuthService)
            :base(CacheService)
        {
            OAuth = OAuthService;
        }

        private readonly IOAuthService OAuth;  

        [HttpGet]
        public ActionResult<OAuthClient> Get() {
            var claim = HttpContext.User.Claims.ElementAt(1);
            return Ok(claim.Value);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public ActionResult Register([FromBody] OAuthClient client) {
            return Ok(OAuth.RegisterClient(client));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("access_token")]
        public ActionResult AccessToken(string authorization_code){
            if(String.IsNullOrEmpty(authorization_code)){
                return Unauthorized();
            }
            var client_id = Cache.Get(authorization_code);
            if(String.IsNullOrEmpty(client_id)){
                return Unauthorized();
            }
            Cache.Delete(authorization_code);
            var client = OAuth.GetClient(client_id);
            if(client == null){
                return Unauthorized();
            }
            var authResponse = OAuth.Authenticate(client);
            if(authResponse == null){
                return Unauthorized();
            }
            return Ok(authResponse);
        }

        // protected ActionResult _authenticate(string Username, string Password){

        //     AuthResponse authResponse = Auth.Authenticate(user);

        //     if(authResponse == null) {
        //         return Unauthorized();
        //     }

        //     return Ok(authResponse);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using services;
using repositories.models;
using services.helpers;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace authentication_server.Controllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        public UsersController(IUserService UserService, IHostingEnvironment hostingEnvironment)
        {
            Users = UserService;
            env = hostingEnvironment;
        }

        public IUserService Users { get; set; }
        private readonly IHostingEnvironment env;

        [HttpGet]
        public ActionResult<User> Get(){
            var UserIdClaim = HttpContext.User.Claims.First(claim => claim.Type == "UserId");
            User user = new User(){
                ID = int.Parse(UserIdClaim.Value),
                Username = HttpContext.User.Identity.Name
            };
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] Credentials credentials){
            if(await Users.Exists(credentials.Username)){
                var  message = $"The username \"{credentials.Username}\" is already taken";
                return BadRequest(message);
            }
            string hashedPassword = SHA256Hash.Compute(credentials.Password);
            return Ok(await Users.Create(credentials.Username, hashedPassword));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public async Task<ActionResult> Authenticate([FromBody] Credentials credentials){
            return await _authenticate(credentials.Username, credentials.Password);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult> Login([FromForm] UserForm userForm) {
            return await _authenticate(userForm.Username, userForm.Password, true);
        }

        private async Task<ActionResult> _authenticate(string Username, string Password, bool performRedirect = false){
            string hashedPassword = SHA256Hash.Compute(Password);
            var user = await Users.Find(Username, hashedPassword);
            AuthResponse authResponse = Users.Authenticate(user);
            
            if(authResponse == null) {
              return Unauthorized("Invalid username or password");
            }
            
            if(performRedirect) {
              var redirectOrigin = "https://www.counter-culture.io";
              if(env.IsDevelopment()){
                  redirectOrigin = "http://localhost:8080";
              }
              return Redirect($"{redirectOrigin}/#token={authResponse.access_token}");
            }

            return Ok(authResponse);
        }

    }
}

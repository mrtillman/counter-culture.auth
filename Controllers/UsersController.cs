using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return Ok(HttpContext.User.Identity.Name);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Post([FromBody] Credentials credentials){
            if(Users.Exists(credentials.Username)){
                var  message = $"The username \"{credentials.Username}\" is already taken";
                return BadRequest(message);
            }
            string hashedPassword = SHA256Hash.Compute(credentials.Password);
            return Ok(Users.Create(credentials.Username, hashedPassword));
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
            string accessToken = Users.Authenticate(user);
            
            if(string.IsNullOrEmpty(accessToken)) {
              return BadRequest("Invalid username or password");
            }
            
            if(performRedirect) {
              var redirectOrigin = "https://www.counter-culture.io";
              if(env.IsDevelopment()){
                  redirectOrigin = "http://localhost:8080";
              }
              return Redirect($"{redirectOrigin}/#token={accessToken}");
            }

            return Ok(accessToken);
        }

    }
}

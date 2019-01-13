using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
        public UsersController(IUserService UserService)
        {
            Users = UserService;
        }
        public IUserService Users { get; set; }

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
        public ActionResult Authenticate([FromBody] Credentials credentials){
            string hashedPassword = SHA256Hash.Compute(credentials.Password);
            var user = Users.Find(credentials.Username, hashedPassword);
            string accessToken = Users.Authenticate(user);
            
            if(string.IsNullOrEmpty(accessToken)) 
                return BadRequest("Invalid username or password");

            return Ok(accessToken);
        }

    }
}

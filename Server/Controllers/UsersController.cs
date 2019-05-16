using System;
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
    public class UsersController : BaseController
    {
        public UsersController(
            ICacheService CacheService,
            IUserService UserService)
            :base(CacheService)
        {
            Users = UserService;
        }

        private readonly IUserService Users;

        [HttpGet]
        public ActionResult<User> Get() {
            var UserIdClaim = HttpContext.User.Claims.First(claim => claim.Type == "UserId");
            User user = new User(){
                ID = int.Parse(UserIdClaim.Value),
                Username = HttpContext.User.Identity.Name
            };
            return Ok(user);
        }

    }
}

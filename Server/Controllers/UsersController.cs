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
    public class UsersController : BaseController
    {
        public UsersController(
            ICacheService CacheService,
            ILogger<UsersController> LoggerService,
            IUserService UserService)
            :base(CacheService)
        {
            Users = UserService;
            Logger = LoggerService;
        }

        private IUserService Users { get; set; }
        private ILogger<UsersController> Logger { get; set; }

        [HttpGet]
        public ActionResult<User> Get() {
            if (!User.Identity.IsAuthenticated){
                return Unauthorized();
            }
            var claim = HttpContext.User.Claims.ElementAt(1);
            if(int.TryParse(claim.Value, out var user_id)){
                return Ok(Users.FindById(user_id));
            }
            return null;
        }

    }
}

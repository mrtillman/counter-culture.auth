using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using CounterCulture.Services;
using CounterCulture.Constants;
using CounterCulture.Models;
using CounterCulture.Utilities;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;

namespace CounterCulture.Controllers
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : BaseController
    {
        public UsersController(
            ILogger<UsersController> LoggerService,
            UserManager<IdentityUser> UserService)
        {
            Users = UserService;
            Logger = LoggerService;
        }
        private readonly UserManager<IdentityUser> Users;
        private ILogger<UsersController> Logger { get; set; }

        [HttpGet]
        public async Task<IActionResult> Get() {
            if (!User.Identity.IsAuthenticated){
                return Unauthorized();
            }

            IdentityUser user = null;

            try{
                var claimType = OpenIdConnectConstants.Claims.Subject;
                var userId = User.FindFirstValue(claimType);
                user = await Users.FindByIdAsync(userId);
            } catch (Exception ex){
                Console.WriteLine(ex);
                throw ex;
            }
            
            return Ok(user);
        }

    }
}

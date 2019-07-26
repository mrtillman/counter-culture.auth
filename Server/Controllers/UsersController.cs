// using System;
// using System.Web.Http;
// using System.Collections.Generic;
// using System.Linq;
// using System.Security.Principal;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.Logging;
// using CounterCulture.Services;
// using CounterCulture.Constants;
// using CounterCulture.Models;
// using CounterCulture.Utilities;
// using Microsoft.AspNetCore.Mvc.Versioning;
// using Microsoft.AspNetCore.Authentication.JwtBearer;

// namespace CounterCulture.Controllers
// {
    
//     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//     public class UsersController : BaseController
//     {
//         public UsersController(
//             ICacheService CacheService,
//             ILogger<UsersController> LoggerService,
//             UserManager<AppUser> UserService)
//             :base(CacheService)
//         {
//             Users = UserService;
//             Logger = LoggerService;
//         }
//         private readonly UserManager<AppUser> Users;
//         private ILogger<UsersController> Logger { get; set; }

//         [HttpGet]
//         public async Task<IActionResult> Get() {
//             if (!User.Identity.IsAuthenticated){
//                 return Unauthorized();
//             }
            
//             var claim = HttpContext.User.Claims.ElementAt(1);
//             var userId = claim.Value;

//             if(string.IsNullOrEmpty(userId)){
//                 return Unauthorized();
//             }
            
//             var user = await Users.FindByIdAsync(userId);

//             if(user == null){
//                 return Unauthorized();
//             }

//             return Ok(user);
//         }

//     }
// }

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AspNet.Security.OpenIdConnect.Primitives;

namespace Presentation.Controllers
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

            var userId = User.FindFirstValue(OpenIdConnectConstants.Claims.Subject);
            IdentityUser user = await Users.FindByIdAsync(userId);
            
            return Ok(user);
        }

    }
}

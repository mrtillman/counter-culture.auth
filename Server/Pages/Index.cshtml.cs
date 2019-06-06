using System;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using CounterCulture.Services;
using CounterCulture.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace CounterCulture.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(
            ICacheService CacheService,
            IConfiguration ConfigurationService,
            IHostingEnvironment hostingEnvironment,
            UserManager<AppUser> UserService,
            ILogger<IndexModel> LoggerService)
        {
            Cache = CacheService;
            Config = ConfigurationService;
            env = hostingEnvironment;
            Users = UserService;
            Logger = LoggerService;
        }

        private ILogger<IndexModel> Logger { get; set; }

        private IConfiguration Config { get; set; }
        private ICacheService Cache { get; set; }
        private IHostingEnvironment env  { get; set; }
        private UserManager<AppUser> Users  { get; set; }

        public async Task<IActionResult> OnPostLogin([FromForm] User creds)
        {
            var referer = Request.Headers["referer"].ToString();
            var queryString = new Uri(referer).Query;
            var _authReq = HttpUtility.ParseQueryString(queryString);
            var state = _authReq.Get("state");

            var client_id = Config["ccult_client_id"];

            AuthRequest authReq = new AuthRequest() {
                client_id = client_id,
                state = state
            };

            var user = await Users.FindByEmailAsync(creds.Username);
            if(!await Users.CheckPasswordAsync(user, creds.Password)){
                return Unauthorized();
            }

            var code = Guid.NewGuid().ToString();

            Cache.Set(code, $"{client_id}:{user.Id}");

            var homePage = "https://www.counter-culture.io";
            if(env.IsDevelopment()){
                homePage = $"http://localhost:8080";
            }

            return Redirect($"{homePage}#code={code}&state={authReq.state}");
            
        }
    }
}
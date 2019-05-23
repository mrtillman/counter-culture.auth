using System;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CounterCulture.Services;
using CounterCulture.Models;
using CounterCulture.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace CounterCulture.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(
            ICacheService CacheService,
            IHostingEnvironment hostingEnvironment,
            IUserService UserService,
            ILogger<IndexModel> LoggerService)
        {
            Cache = CacheService;
            env = hostingEnvironment;
            Users = UserService;
            Logger = LoggerService;
        }

        ILogger<IndexModel> Logger { get; set; }

        private ICacheService Cache { get; set; }
        private IHostingEnvironment env  { get; set; }
        private IUserService Users  { get; set; }

        public IActionResult OnPostLogin([FromForm] User creds)
        {
            
            var referer = Request.Headers["referer"].ToString();
            var queryString = new Uri(referer).Query;
            var _authReq = HttpUtility.ParseQueryString(queryString);
            var state = _authReq.Get("state");

            var client_id = Environment.GetEnvironmentVariable("ccult_client_id");

            AuthRequest authReq = new AuthRequest() {
                client_id = client_id,
                state = state
            };

            var hashedPassword = SHA256Hash.Compute(creds.Password);
            var user = Users.Find(creds.Username, hashedPassword);
            
            if(user == null){
                return Unauthorized();
            }

            var code = Guid.NewGuid().ToString();

            Cache.Set(code, $"{client_id}:{user.ID}");

            var homePage = "https://www.counter-culture.io";
            if(env.IsDevelopment()){
                homePage = $"http://localhost:8080";
            }

            return Redirect($"{homePage}#code={code}&state={authReq.state}");
            
        }
    }
}
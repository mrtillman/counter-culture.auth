using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CounterCulture.Services;
using CounterCulture.Models;
using CounterCulture.Utilities;

namespace CounterCulture.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(
            ICacheService CacheService,
            IHostingEnvironment hostingEnvironment,
            IUserService UserService)
        {
            Cache = CacheService;
            env = hostingEnvironment;
            Users = UserService;
        }

        private ICacheService Cache { get; set; }
        private IHostingEnvironment env  { get; set; }
        private IUserService Users  { get; set; }

        public IActionResult OnPostLogin([FromForm] Credentials creds)
        {
            var hashedPassword = SHA256Hash.Compute(creds.Password);
            var user = Users.Find(creds.Username, hashedPassword);
            
            if(user == null){
                return Unauthorized();
            }

            var authorization_code = Guid.NewGuid().ToString();
            var client_id = Environment.GetEnvironmentVariable("CLIENT_ID");
            
            Cache.Set(authorization_code, $"{client_id}:{user.ID}");

            var redirectOrigin = "https://www.counter-culture.io";
            if(env.IsDevelopment()){
                redirectOrigin = $"http://localhost:8080";
            }
            
            return Redirect($"{redirectOrigin}#code={authorization_code}");
            
        }
    }
}
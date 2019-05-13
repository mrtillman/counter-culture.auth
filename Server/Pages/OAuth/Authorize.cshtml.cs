using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CounterCulture.Services;
using CounterCulture.Repositories.Models;

namespace RazorPagesIntro.Pages
{
    public class AuthorizeModel : PageModel
    {
        public AuthorizeModel(
            IUserService UserService, 
            IOAuthService OAuthService, 
            IHostingEnvironment hostingEnvironment,
            ICacheService CacheService)
        {
            Users = UserService;
            OAuth = OAuthService;
            env = hostingEnvironment;
            Client = new OAuthClient();
            Cache = CacheService;
        }

        public IUserService Users { get; set; }
        public IOAuthService OAuth { get; set; }
        public OAuthClient Client { get; set; }

        private readonly IHostingEnvironment env;
        private readonly ICacheService Cache;

        public void OnGet([FromQuery] AuthRequest authReq)
        {
            Client = OAuth.GetClient(authReq.client_id);
            //string code = "authorization_code";
            //Cache.Set("code", code);
            // Console.WriteLine();
            // Console.WriteLine(Cache.Get("code"));
            // Console.WriteLine();
        }
    }
}
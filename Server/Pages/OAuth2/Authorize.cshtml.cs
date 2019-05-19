using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CounterCulture.Services;
using CounterCulture.Models;

namespace CounterCulture.Pages
{
    public class AuthorizeModel : PageModel
    {
        public AuthorizeModel(
            ICacheService CacheService, 
            IOAuthService OAuthService,
            IUserService UserService)
        {
            Cache = CacheService;
            OAuth = OAuthService;
            Users = UserService;
        }

        private ICacheService Cache { get; set; }
        private IOAuthService OAuth { get; set; }
        private IUserService Users { get; set; }

        public OAuthClient Client { get; set; }

        public void OnGet([FromQuery] AuthRequest authReq)
        {
            Client = OAuth.GetClient(authReq.client_id);
        }

        public IActionResult OnPostClientAuthorization(string client_id, string redirect_uri) {
            var authorization_code = Guid.NewGuid().ToString();
            Cache.Set(authorization_code, client_id);
            return Redirect($"{redirect_uri}#code={authorization_code}");
        }
    }
}
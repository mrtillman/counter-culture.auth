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
            // TODO: prompt for user login
            Client = OAuth.GetClient(authReq.client_id);
        }

        public IActionResult OnPostClientAuthorization(string client_id, string redirect_uri) {
            var authorization_code = Guid.NewGuid().ToString();
            // TODO: get user id from the 
            // current user who is logged in
            var userID = 12; 
            Cache.Set(authorization_code, $"{client_id}:{userID}");
            return Redirect($"{redirect_uri}#authorization_code={authorization_code}");
        }
    }
}
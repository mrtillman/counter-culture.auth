using System;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CounterCulture.Services;
using CounterCulture.Models;

namespace CounterCulture.Pages
{
    public class AuthorizeModel : PageModel
    {
        public AuthorizeModel(
            ICacheService CacheService,
            IOAuthService OAuthService)
        {
            Cache = CacheService;
            OAuth = OAuthService;
        }

        private ICacheService Cache { get; set; }
        private IOAuthService OAuth { get; set; }
        public OAuthClient Client { get; set; }

        public void OnGet([FromQuery] AuthRequest authReq)
        {
            // TODO: prompt for user login
            Client = OAuth.GetClient(authReq.client_id);
            ViewData.Add("state", authReq.state);
        }

        public IActionResult OnPostClientAuthorization(
            [FromForm] AuthRequest authReq) 
        {

            var code = Guid.NewGuid().ToString();

            // TODO: get user id from the 
            // current user who is logged in
            var userID = 9; 
            
            Cache.Set(code, $"{authReq.client_id}:{userID}");

            return Redirect($"{authReq.redirect_uri}#code={code}&state={authReq.state}");
        }
    }
}

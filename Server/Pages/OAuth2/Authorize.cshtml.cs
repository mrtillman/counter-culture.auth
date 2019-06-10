using System;
using System.Web;
using System.Linq;
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

        public IActionResult OnGet([FromQuery] AuthRequest authReq)
        {
            if(User.Identity.IsAuthenticated){
                Client = OAuth.GetClient(authReq.client_id);
                ViewData.Add("state", authReq.state);
            } else {
                // TODO: simplify using extensions/helper module
                // ex: helper.GetCurrentUrl()
                var currentUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
                var redirect_uri = HttpUtility.UrlEncode(currentUrl);
                return Redirect($"~/?redirect_uri={redirect_uri}");
            }
            return Page();
        }

        public IActionResult OnPostClientAuthorization(
            [FromForm] AuthRequest authReq) 
        {

            if(!User.Identity.IsAuthenticated){
                return Unauthorized();
            }

            var code = Guid.NewGuid().ToString();

            var userId = User.Claims.First().Value;            
            
            Cache.Set(code, $"{authReq.client_id}:{userId}");

            return Redirect($"{authReq.redirect_uri}#code={code}&state={authReq.state}");
        }
    }
}

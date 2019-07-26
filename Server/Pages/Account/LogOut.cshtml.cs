using System;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CounterCulture.Services;
using CounterCulture.Models;
using CounterCulture.Utilities;
using System.Security.Claims;
using System.Security.Principal;
using IdentityServer4.Test;

namespace CounterCulture.Pages
{
    public class LogOutModel : PageModel
    {
        public LogOutModel(
            UserManager<TestUser> UserService,
            SignInManager<TestUser> SignInManager)
        {
            AppSignIn = SignInManager;
            Users = UserService;
        }

        TestUser TestUser { get; set; }
        UserManager<TestUser> Users { get; set; }
        SignInManager<TestUser> AppSignIn { get; set; }

        public bool LoggedOut { get; set; }
        public string redirect_uri { get; set; }  

        public async Task<IActionResult> OnPostLogOut()
        {
            // TODO: modularize
            var referer = Request.Headers["referer"].ToString();
            var queryString = new Uri(referer).Query;
            var queryStringValues = HttpUtility.ParseQueryString(queryString);
            redirect_uri = queryStringValues.Get("redirect_uri");

            if(User.Identity.IsAuthenticated) {
                await AppSignIn.SignOutAsync();
            }
            
            LoggedOut = true;

            return Page();
        }

    }
}
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

namespace CounterCulture.Pages
{
    public class HomeModel : PageModel
    {
        public HomeModel(
            ILogger<HomeModel> LoggerService,
            UserManager<AppUser> UserService)
        {
            Logger = LoggerService;
            Users = UserService;
        }

        AppUser AppUser { get; set; }
        ILogger<HomeModel> Logger { get; set; }
        UserManager<AppUser> Users { get; set; }

        public string Username { get; set; } = "Unauthorized";

        public void OnGet(){
            if(User.Identity.IsAuthenticated){
                Username = User.Claims.First().Value;
            }
        }

    }
}
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CounterCulture.Services;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(
            IHostingEnvironment hostingEnvironment)
        {
            env = hostingEnvironment;
        }

        private readonly IHostingEnvironment env;

        public void OnGet(){

        }

        public IActionResult OnPostLogin(User user)
        {
            var redirectOrigin = "https://www.counter-culture.io";
            if(env.IsDevelopment()){
                redirectOrigin = $"http://localhost:8080";
            }
            // user.Username
            var authorization_code = "123555";
            //Cache.Set(user.Username, authorization_code);
            return Redirect($"{redirectOrigin}#code={authorization_code}");
        }
    }
}
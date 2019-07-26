using System;
using System.Web;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CounterCulture.Services;
using CounterCulture.Models;
using CounterCulture.Utilities;
using IdentityServer4.Test;
using IdentityServer4.Quickstart.UI;
using IdentityModel;

namespace CounterCulture.Pages
{
    public class RegisterModel : PageModel
    {
        public RegisterModel(
            ILogger<RegisterModel> LoggerService,
            UserManager<TestUser> UserService)
        {
            Logger = LoggerService;
            Users = UserService;
            TestUser = new TestUser();
        }

        public TestUser TestUser { get; set; }
        public ILogger<RegisterModel> Logger { get; set; }
        public UserManager<TestUser> Users { get; set; }

        //public async Task<IActionResult> OnPostSubmitRegistration([FromForm] TestUser user)
        public async Task<IActionResult> OnPostSubmitRegistration([FromForm] TestUser user)
        {
            // user.UserName = user.Email.Split('@')[0];
            
            user.SubjectId = Guid.NewGuid().ToString();
            var result = await Users.CreateAsync(user, user.Password);

            var claims = new List<Claim>{
                new Claim(JwtClaimTypes.Role, "counter")
            };

            if(ModelState.IsValid && result.Succeeded){
                
                return LocalRedirect("/account/login");

            } else {
                if(result.Errors.Count() > 0){
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                } else {
                    ModelState.AddModelError(string.Empty, "Unknown Error");
                }
                
            }

            return Page();
            
        }
    }
}
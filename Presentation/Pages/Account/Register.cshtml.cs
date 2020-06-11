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
using Presentation.Services;
using Presentation.Models;
using Presentation.Utilities;
using IdentityServer4.Test;
using IdentityServer4.Quickstart.UI;
using IdentityModel;

namespace Presentation.Pages
{
    public class RegisterModel : PageModel
    {
        public RegisterModel(
            ILogger<RegisterModel> LoggerService,
            UserManager<IdentityUser> UserService)
        {
            Logger = LoggerService;
            Users = UserService;
            Registrant = new LoginViewModel();
        }

        public LoginViewModel Registrant { get; set; }
        public ILogger<RegisterModel> Logger { get; set; }
        public UserManager<IdentityUser> Users { get; set; }
        public async Task<IActionResult> OnPostSubmitRegistration([FromForm] LoginViewModel model)
        {
            var user = new IdentityUser(model.Username);
            
            user.Email = model.Username;
            
            var result = await Users.CreateAsync(user, model.Password);

            var claims = new List<Claim>{
                new Claim(JwtClaimTypes.Role, "counter")
            };

            if(ModelState.IsValid && result.Succeeded){
                
                return LocalRedirect("/");

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
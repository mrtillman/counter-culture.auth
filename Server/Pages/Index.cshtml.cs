using System;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using CounterCulture.Services;
using CounterCulture.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CounterCulture.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(
            ICacheService CacheService,
            IConfiguration ConfigurationService,
            IHostingEnvironment hostingEnvironment,
            UserManager<AppUser> UserService,
            SignInManager<AppUser> SignInManager,
            ILogger<IndexModel> LoggerService)
        {
            Cache = CacheService;
            Config = ConfigurationService;
            env = hostingEnvironment;
            Users = UserService;
            Logger = LoggerService;
            AppSignIn = SignInManager;
        }

        ILogger<IndexModel> Logger { get; set; }

        IConfiguration Config { get; set; }
        ICacheService Cache { get; set; }
        IHostingEnvironment env  { get; set; }
        UserManager<AppUser> Users  { get; set; }

        SignInManager<AppUser> AppSignIn { get; set; }
        public void OnGet(){
            if(User.Identity.IsAuthenticated){
                Response.Redirect("/Account/Home");
            }
        }
        
        public async Task<IActionResult> OnPostLogin(string Email, string Password)
        {
            // returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // TODO: simplify with string extensions
                var userName = Email.Split('@')[0];

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, 
                // set lockoutOnFailure: true
                var result = await AppSignIn.PasswordSignInAsync(userName, 
                    Password, isPersistent: true, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    
                    Logger.LogInformation("User logged in.");
        
                    // TODO: modularize
                    var referer = Request.Headers["referer"].ToString();
                    var queryString = new Uri(referer).Query;
                    var queryStringValues = HttpUtility.ParseQueryString(queryString);
                    var redirect_uri = queryStringValues.Get("redirect_uri");

                    if(String.IsNullOrEmpty(redirect_uri)){
                        redirect_uri = "/Account/Home";
                    }
                    
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    var principal = new ClaimsPrincipal(identity);
                    
                    await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    
                    return Redirect(redirect_uri);
                }
                // TODO: implement 2fa + lockout
                // if (result.RequiresTwoFactor)
                // {
                //     return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                // }
                // if (result.IsLockedOut)
                // {
                //     _logger.LogWarning("User account locked out.");
                //     return RedirectToPage("./Lockout");
                // }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
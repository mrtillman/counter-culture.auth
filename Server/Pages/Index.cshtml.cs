using System;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using CounterCulture.Services;
using CounterCulture.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

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

        /*
        public async Task<IActionResult> OnPostLogin([FromForm] AppUser user)
        {
            var referer = Request.Headers["referer"].ToString();
            var queryString = new Uri(referer).Query;
            var _authReq = HttpUtility.ParseQueryString(queryString);
            var state = _authReq.Get("state");

            var client_id = Config["ccult_client_id"];

            AuthRequest authReq = new AuthRequest() {
                client_id = client_id,
                state = state
            };
            var _user = await Users.FindByEmailAsync(user.Email);
            if(_user == null){
                return Unauthorized();
            }

            if(!await Users.CheckPasswordAsync(_user, user.Password)){
                return Unauthorized();
            }

            var code = Guid.NewGuid().ToString();

            Cache.Set(code, $"{client_id}:{user.Id}");

            var homePage = "https://www.counter-culture.io";
            if(env.IsDevelopment()){
                homePage = $"http://localhost:8080";
            }

            return Redirect($"{homePage}#code={code}&state={authReq.state}");

        }
        */

        public async Task<IActionResult> OnPostLogin([FromForm] AppUser user)
        {
            // returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // TODO: simplify with string extensions
                var userName = user.Email.Split('@')[0];

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, 
                // set lockoutOnFailure: true
                var result = await AppSignIn.PasswordSignInAsync(userName, 
                    user.Password, isPersistent: false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    
                    Logger.LogInformation("User logged in.");

                    var homePage = "https://www.counter-culture.io";
                    
                    if(env.IsDevelopment()){
                        homePage = $"http://localhost:8080";
                    }

                    return Redirect(homePage);
                }
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
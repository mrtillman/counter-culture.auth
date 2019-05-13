using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CounterCulture.Services;
using CounterCulture.Repositories.Models;

namespace RazorPagesIntro.Pages
{
    public class AuthorizeModel : PageModel
    {
        public AuthorizeModel(
            IUserService UserService, 
            IOAuthService OAuthService, 
            IHostingEnvironment hostingEnvironment)
        {
            Users = UserService;
            OAuth = OAuthService;
            env = hostingEnvironment;
            Client = new OAuthClient();
        }

        public IUserService Users { get; set; }
        public IOAuthService OAuth { get; set; }
        public OAuthClient Client { get; set; }
        private readonly IHostingEnvironment env;

        public void OnGet([FromQuery] AuthRequest authReq)
        {
            Client = OAuth.GetClient(authReq.client_id);
            Console.WriteLine(Client.client_id);
        }
    }
}
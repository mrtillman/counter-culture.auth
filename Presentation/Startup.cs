using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presentation.Configuration;
using Presentation.Services;
using Presentation.Constants;

namespace Presentation
{
  public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory _LoggerFactory, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            env = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment env { get; set; }
        private string mySqlConnectionString {
            get {
                return Configuration["ConnectionStrings:DefaultMySQLConnection"];
            }
        }

        private string appSecret {
            get {
                return Configuration["AppSecret"];
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mode = env.IsProduction() ? ENV.PROD : ENV.DEV;

            services.AddApiVersioning();

            services.AddMvc()
                    .AddJsonOptions(options => {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });
            
            var ServerUrls = new ServerUrls(env);

            // add client-side app cookie
            // and display cookie policy
            services.ConfigureAppCookieAndPolicy();

            // cookie + jwt authentication
            services.ConfigureAuthentication(ServerUrls, appSecret, mode);

            // membership system to manage users + roles
            services.ConfigureAspNetIdentity(mySqlConnectionString);

            // oauth 2.0 implementation
            services.ConfigureIdentityServer4(mySqlConnectionString, ServerUrls);
            
            // seed demo users, oauth 2.0 clients + resources
            services.AddTransient<IStartupFilter, OnStartupFilter>();

            // add app version service
            services.AddTransient<IAppVersionService, AppVersionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(policy => {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                policy.WithHeaders("Origin", "X-Requested-With", "Content-Type", "Accept", "Authorization");
            });
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions(){
                RequestPath = new PathString("")
            });
            app.UseMvcWithDefaultRoute();
            app.UseIdentityServer();
        }
    }
}
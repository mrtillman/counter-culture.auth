using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using MySql.Data.MySqlClient;
using CounterCulture.Repositories;
using CounterCulture.Models;
using CounterCulture.Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace CounterCulture
{
  public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddIdentityCore<AppUser>();
            services.AddDbContext<SecureDbContext>(options => {
                options.UseMySql(
                    Configuration["ConnectionStrings:DefaultMySQLConnection"]);
            });
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<SecureDbContext>()
                    .AddDefaultTokenProviders();
            //services.AddDefaultIdentity<AppUser>();
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/";
                options.AccessDeniedPath = "/";
                options.SlidingExpiration = true;
            });
            services.AddApiVersioning();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddMvc()
                    .AddJsonOptions(options => {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });
            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                // options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                // options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(options =>
            {
                options.Audience = "http://counter-culture.io";
                options.Authority = "http://counter-culture.io";
                var signingKey = Encoding.ASCII
                                .GetBytes(Configuration["AppSecret"]);
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
            ConnectionMultiplexer redisConnection = 
             ConnectionMultiplexer
            .Connect(Configuration["ConnectionStrings:DefaultRedisConnection"]);
            services.AddSingleton<IConnectionMultiplexer>(redisConnection);
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IOAuthRepository, OAuthRepository>();
            services.AddScoped<IOAuthService, OAuthService>();
            services.AddScoped<IUserStore<AppUser>, AppUserStore>();
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
            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions(){
                RequestPath = new PathString("")
            });
            app.UseMvcWithDefaultRoute();
        }
    }
}

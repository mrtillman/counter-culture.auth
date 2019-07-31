using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CounterCulture.Repositories;
using CounterCulture.Models;
using CounterCulture.Services;
using CounterCulture.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNet.Security.OpenIdConnect.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace CounterCulture
{
  public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory _LoggerFactory)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private string mySqlConnection {
            get {
                return Configuration["ConnectionStrings:DefaultMySQLConnection"];
            }
        }
        private string migrationsAssembly {
            get {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddJsonOptions(options => {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });
                    
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<SecureDbContext>(options => {
                options.UseMySql(
                    Configuration["ConnectionStrings:DefaultMySQLConnection"], 
                        mySqlOptions => mySqlOptions.MigrationsAssembly(migrationsAssembly));
            });
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
            })
                    .AddEntityFrameworkStores<SecureDbContext>()
                    .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/";
                options.SlidingExpiration = true;
            });
            services.AddApiVersioning();
            
            services
            .AddAuthentication( options => {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            //.AddCookie("Cookie")
            .AddCookie(options => {
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "";
                options.SlidingExpiration = true;
                options.Cookie.Expiration = TimeSpan.FromDays(1);  
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;

                options.ClientId = "countercultureapp";
                options.SaveTokens = true;
            })
            // .AddIdentityServerAuthentication(options => {
            //     options.Authority = "http://localhost:5000";
            //     options.ApiName = "counterculturesecure";
            //     options.SupportedTokens = SupportedTokens.Both;
            //     options.RequireHttpsMetadata = false;
            // })
            .AddJwtBearer(options =>
            {
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
            
            // ConnectionMultiplexer redisConnection = 
            //  ConnectionMultiplexer
            // .Connect(Configuration["ConnectionStrings:DefaultRedisConnection"]);
            // services.AddSingleton<IConnectionMultiplexer>(redisConnection);
            //services.AddScoped<ITestUserRepository, TestUserRepository>();
            services.AddScoped<ICacheService, CacheService>();
            //services.AddScoped<IUserStore<TestUser>, CounterCulture.Services.TestUserStore>();
            services.AddIdentityServer()
            .AddOperationalStore(options =>
                options.ConfigureDbContext = builder => 
                    builder.UseMySql(mySqlConnection, sqlOptions => 
                        sqlOptions.MigrationsAssembly(migrationsAssembly)))
            .AddConfigurationStore(options =>
                options.ConfigureDbContext = builder =>
                    builder.UseMySql(mySqlConnection, sqlOptions => 
                        sqlOptions.MigrationsAssembly(migrationsAssembly)))
            .AddDeveloperSigningCredential();
            //.AddAspNetIdentity<IdentityUser>();
            services.AddTransient<IStartupFilter, OnStartupFilter>();
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
            app.UseIdentityServer();
        }
    }
}
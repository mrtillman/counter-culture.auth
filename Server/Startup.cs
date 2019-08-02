using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CounterCulture.Repositories;
using CounterCulture.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Test;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CounterCulture
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.AddIdentityCore<TestUser>();
            services.AddDbContext<SecureDbContext>(options => {
                options.UseMySql(
                    Configuration["ConnectionStrings:DefaultMySQLConnection"], 
                        mySqlOptions => mySqlOptions.MigrationsAssembly(migrationsAssembly));
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
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
            services.AddMvc()
                    .AddJsonOptions(options => {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });
            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var signingKey = Encoding.ASCII
                            .GetBytes(Configuration["AppSecret"]);
            var _tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    // IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:5000",
                    ValidateAudience = true,
                    ValidAudience = "http://localhost:5000/resources"
                };

            services.AddAuthentication()
            .AddCookie(options => options.SlidingExpiration = true)
            .AddJwtBearer(options =>
            {
                options.Authority = env.IsProduction() ? "https://secure.counter-culture.io" : "http://localhost:5000";
                options.Audience = "WebAPI";
                options.RequireHttpsMetadata = env.IsProduction();
                options.SaveToken = true;
                options.TokenValidationParameters = _tokenValidationParameters;
            });
            
            // ConnectionMultiplexer redisConnection = 
            //  ConnectionMultiplexer
            // .Connect(Configuration["ConnectionStrings:DefaultRedisConnection"]);
            // services.AddSingleton<IConnectionMultiplexer>(redisConnection);
            services.AddScoped<ITestUserRepository, TestUserRepository>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IUserStore<TestUser>, CounterCulture.Services.TestUserStore>();
            services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddOperationalStore(options =>
                options.ConfigureDbContext = builder => 
                    builder.UseMySql(mySqlConnection, sqlOptions => 
                        sqlOptions.MigrationsAssembly(migrationsAssembly)))
            .AddConfigurationStore(options =>
                options.ConfigureDbContext = builder =>
                    builder.UseMySql(mySqlConnection, sqlOptions => 
                        sqlOptions.MigrationsAssembly(migrationsAssembly)))
            .AddAspNetIdentity<IdentityUser>();
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
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions(){
                RequestPath = new PathString("")
            });
            app.UseMvcWithDefaultRoute();
            app.UseIdentityServer();
        }
    }
}
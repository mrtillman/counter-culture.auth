using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using CounterCulture.Repositories;
using CounterCulture.Models;
using CounterCulture.Services;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Versioning;
using CounterCulture.Helpers;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace CounterCulture
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory _LoggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = _LoggerFactory;
        }

        public IConfiguration Configuration { get; }
        private ILoggerFactory LoggerFactory { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSecrets = Configuration.Get<AppSecrets>();
            services.Configure<AppSecrets>(Configuration);
            services.AddMvc()
                    .AddJsonOptions(options => {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });
            services.AddApiVersioning();
            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = "BasicAuthentication";
            })
            .AddJwtBearer(x =>
            {
                
                var key = Encoding.ASCII.GetBytes(appSecrets.Secret);
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            MySqlConnection connection = new MySqlConnection(appSecrets.MySQLConnectionString);
            UserRepository userRepo = new UserRepository(
                connection, appSecrets, LoggerFactory.CreateLogger<UserRepository>());
            OAuthRepository oauthRepo = new OAuthRepository(
                connection, appSecrets, LoggerFactory.CreateLogger<OAuthRepository>());
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(appSecrets.RedisConnectionString);
            services.Add(new ServiceDescriptor(typeof(IOAuthRepository),
             provider => RepoProxy<IOAuthRepository>.Create(oauthRepo),
             ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IUserRepository),
             provider => RepoProxy<IUserRepository>.Create(userRepo),
             ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(ICacheService),
             provider => new CacheService(redis),
             ServiceLifetime.Scoped));
            services.AddScoped<IOAuthService, OAuthService>();
            services.AddScoped<IUserService, UserService>();
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

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using CounterCulture.Repositories.Models;
using CounterCulture.Services;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Versioning;
using CounterCulture.Secure.Helpers;

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
            var appSecrets = Configuration.Get<AppSecrets>();
            services.Configure<AppSecrets>(Configuration);
            services.AddMvc(options => options.OutputFormatters.Add(new HtmlOutputFormatter()));
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
            UserRepository userRepo = new UserRepository(connection, appSecrets);
            OAuthRepository oauthRepo = new OAuthRepository(connection, appSecrets);
            services.Add(new ServiceDescriptor(typeof(IUserRepository),
             provider => UserRepoProxy<IUserRepository>.Create(userRepo),
             ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IOAuthRepository),
             provider => UserRepoProxy<IOAuthRepository>.Create(oauthRepo),
             ServiceLifetime.Scoped));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOAuthService, OAuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
        }
    }
}

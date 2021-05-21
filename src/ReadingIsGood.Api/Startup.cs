using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ReadingIsGood.Api.Middleware;
using ReadingIsGood.Core.Options;
using ReadingIsGood.Application;
using ReadingIsGood.Application.Mediator;
using ReadingIsGood.Core;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System;
using System.Text;
using ReadingIsGood.Common.ExceptionHandling;
using System.Net;
using Microsoft.Extensions.Logging;
using ReadingIsGood.Core.Services.Abstractions;

namespace ReadingIsGood.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly string applicationName;
        private readonly string environmentName;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.applicationName = env.ApplicationName;
            this.environmentName = env.EnvironmentName;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServices(this.configuration)
                    .AddMediator()
                    .AddApplicationServices();

            services.AddMvc(options => options.EnableEndpointRouting = false)
                    .AddNewtonsoftJson();

            //TODO: Tavsiye edilen bir yöntem değildir!
            var seedService = services.BuildServiceProvider().GetRequiredService<ISeedService>();

            var settings = this.configuration.GetSection("AuthenticationOptions").Get<AuthenticationOptions>();
            services.Configure<AuthenticationOptions>(this.configuration.GetSection("AuthenticationOptions"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = settings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(settings.SecurityKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = ctx =>
                    {
                        //Gerekirse burada gelen token içerisindeki çeşitli bilgilere göre doğrulma yapılabilir. ve filtreleme yapılabilir.
                        if (ctx.SecurityToken.ValidTo < DateTime.UtcNow)
                        {
                            throw new Exception("Could not get exp claim from token");
                        }

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = ctx =>
                    {
                        //Console.WriteLine("Exception:{0}", ctx.Exception.Message);
                        //return Task.CompletedTask;
                        throw new ReadingIsGoodException("Authentication failed", (HttpStatusCode)ctx.Response.StatusCode, logLevel: LogLevel.Information);
                    }
                };
            });


            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{this.applicationName} ({this.environmentName})", Version = "v1" }))
                    .AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting()
               .UseAuthentication()
               .UseSwagger()
               .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{this.applicationName} V1");
                    c.RoutePrefix = string.Empty;
                    c.DocumentTitle = $"{this.applicationName} ({this.environmentName})";
                })
               .UseMiddleware<ExceptionHandlerMiddleware>()
               .UseMvc();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ReadingIsGood.Api.Middleware;
using ReadingIsGood.Application;
using ReadingIsGood.Application.Mediator;
using ReadingIsGood.Core;

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

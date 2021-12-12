using Application;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shared.Extensions;

namespace HotelAutomationApp.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment) => 
            (Configuration, WebHostEnvironment) = (configuration, webHostEnvironment);
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // services.AddSwaggerGen(q => q.SwaggerDoc("v1", new OpenApiInfo {Title = "WebApi", Version = "v1"}));
            
            services.AddMediatR(typeof(Startup));
            services.AddMediatrHandlers(typeof(Plug).Assembly);
            // services.AddDatabases(Configuration);
            // services.AddIdentity();
            // services.AddAuthenticationSystem(Configuration);
            // services.AddSecurityServices(Configuration);
        }

        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                appBuilder.UseDeveloperExceptionPage();
                // appBuilder.UseSwagger();
                // appBuilder.UseSwaggerUI(q => q.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            // appBuilder.UseAuthenticationSystem();

            appBuilder.UseRouting();

            appBuilder.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
using HotelAutomation.WebApi.Extensions;
using HotelAutomationApp.Application.Auth.Commands;
using HotelAutomationApp.Shared.Extensions;
using HotelAutomationApp.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddControllers().AddNewtonsoftJson();

            services.AddCors();
            
            services.AddSwaggerGen(q => q.SwaggerDoc("v1", new OpenApiInfo {Title = "WebApi", Version = "v1"}));

            services.AddMediatR(typeof(Startup));
            services.AddMediatrHandlers(typeof(CreateTokenCommand).Assembly);
            services.AddDatabases(Configuration);
            services.AddIdentity();
            services.AddAuthenticationSystem(Configuration);
            services.AddSecurityServices(Configuration);
        }

        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                appBuilder.UseDeveloperExceptionPage();
            }
            
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(q => q.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

            appBuilder.UseStaticFiles();

            appBuilder.UseAuthenticationSystem();

            appBuilder.UseRouting();

            appBuilder.UseCors(options =>
            {
                options.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials();
            });
            
            appBuilder.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
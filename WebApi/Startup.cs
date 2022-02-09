using System;
using AutoMapper;
using FluentValidation.AspNetCore;
using HotelAutomation.Application.Common;
using HotelAutomation.WebApi.Extensions;
using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.Auth.Commands;
using HotelAutomationApp.Persistence.Extensions;
using HotelAutomationApp.Shared.Extensions;
using HotelAutomationApp.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(q => 
                    q.RegisterValidatorsFromAssemblies(new []
                    {
                        typeof(CreateTokenCommand).Assembly
                    }));

            services.AddCors();
            
            services.AddSwaggerGen(setup =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            });

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddMaps(typeof(UseCase<>).Assembly);
            });

            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMediatR(typeof(Startup));
            services.AddMediatrHandlers(typeof(CreateTokenCommand).Assembly);
            services.AddPersistence(Configuration);
            services.AddIdentity();
            services.AddSeed();
            services.AddAuthenticationSystem(Configuration);
            services.AddSecurityServices(Configuration);
            services.AddScoped(typeof(DictionaryCrudService<,>));
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

            appBuilder.UseRouting();
            
            appBuilder.UseAuthenticationSystem();

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
using HotelAutomation.Domain.Models.Identity;
using HotelAutomation.Infrastructure.Auth.Constants;
using HotelAutomation.Infrastructure.Auth.Services;
using Infrastructure.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Interfaces.Context;

namespace HotelAutomation.WebApi.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IDbContext, ApplicationDbContext>(options => 
                options.UseNpgsql(configuration.GetConnectionString("Default")));

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddSecurityServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.Configure<JwtTokenFactoryOptions>(configuration.GetSection("Security:Token"));
            services.AddScoped<ITokenFactory, JwtTokenFactory>();
            services.AddScoped<ISecurityContext, AspNetCoreSecurityContext>();

            return services;
        }

        public static IServiceCollection AddAuthenticationSystem(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddAuthentication(opts =>
                {
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(opts =>
                {
                    var options = configuration.GetSection("Security:Token").Get<JwtTokenFactoryOptions>();

                    opts.RequireHttpsMetadata = false;

                    opts.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = options.Issuer,
                        ValidAudience = options.Audience,

                        RequireSignedTokens = true,
                        IssuerSigningKey = options.CreateKey(),

                        NameClaimType = Claims.Subject,
                        RoleClaimType = Claims.Role
                    };

                    opts.MapInboundClaims = false;
                });

            services.AddAuthorization(opts =>
            {
                opts.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            return services;
        }
    }
}
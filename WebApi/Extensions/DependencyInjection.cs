using HotelAutomationApp.Application.Auth.Constants;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Constants;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Persistence.Context;
using HotelAutomationApp.WebApi.Seeds;
using HotelAutomationApp.WebApi.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelAutomationApp.WebApi.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 1;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                })
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
                
                opts.AddPolicy(AuthorizationPolicies.RequireAdminRole, policy =>
                {
                    policy.RequireRole(Roles.Admin, Roles.Root);
                });
                
                opts.AddPolicy(AuthorizationPolicies.RequireRootRole, policy =>
                {
                    policy.RequireRole(Roles.Root);
                });
            });

            return services;
        }

        public static IServiceCollection AddSeed(this IServiceCollection services)
        {
            services.AddScoped<Seed>();
            services.AddScoped<IdentitySeed>();

            return services;
        }
    }
}
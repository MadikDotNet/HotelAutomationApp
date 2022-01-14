using System;
using System.Collections.Generic;
using System.Text;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Constants;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace HotelAutomationApp.Infrastructure.Auth.Services
{
    public class JwtTokenFactory : ITokenFactory
    {
        private readonly JwtTokenFactoryOptions _options;
        private readonly JsonWebTokenHandler _tokenHandler;

        public JwtTokenFactory(IOptions<JwtTokenFactoryOptions> options)
        {
            _options = options.Value;
            _tokenHandler = new();
        }

        public string CreateToken(User user)
        {
            var claims = GetClaims(user);

            return CreateToken(claims);
        }
        
        public string CreateToken(Dictionary<string, object> claims)
        {
            var signingCredentials = new SigningCredentials(
                _options.CreateKey(),
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.Add(_options.TokenLifeTime),
                SigningCredentials = signingCredentials,
            };

            return _tokenHandler.CreateToken(tokenDescriptor);
        }

        private Dictionary<string, object> GetClaims(User user)
        {
            return new Dictionary<string, object>
            {
                [Claims.Subject] = user.Id
            };
        }
    }

    public class JwtTokenFactoryOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
        public string SecretKey { get; set; }

        public SymmetricSecurityKey CreateKey()
        {
            return new(Encoding.ASCII.GetBytes(SecretKey));
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Services.Contract.IUserServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Implementation.UserServices
{
    public class TokenService : ITokenService
    {
        private readonly string _tokenSecret;
        private readonly double _tokenExpirationMinutes;

        public TokenService(IConfiguration configuration)
        {
            _tokenSecret = configuration["JwtSettings:SecretKey"];
            _tokenExpirationMinutes = Convert.ToDouble(configuration["JwtSettings:TokenExpirationMinutes"]);
        }


        public string GenerateToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSecret);

            if(user.UserName == null)
            {
                throw new Exception("Invalid username ");
            }

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

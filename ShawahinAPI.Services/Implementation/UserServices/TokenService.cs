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
            _tokenSecret = configuration["JwtSettings:Key"];
            _tokenExpirationMinutes = Convert.ToDouble(configuration["JwtSettings:TokenExpirationMinutes"]);
        }


        public string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_tokenSecret);

            if (user.UserName == null)
            {
                throw new Exception("Invalid username");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id.ToString())
            };

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tokenExpirationMinutes),
                Issuer = "ShawahinAPi",
                Audience = "Shawahin_Users",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

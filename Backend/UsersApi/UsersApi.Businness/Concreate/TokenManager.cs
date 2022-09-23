using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Users.Businness.Abstract;

namespace Users.Businness.Concreate
{
    public class TokenManager : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(string username, string password)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           // var expiry = DateTime.Now.AddDays(int.Parse(_configuration["JwtExpiryInDays"].ToString()));
            var expiry = DateTime.Now.AddMinutes(int.Parse(_configuration["JwtExpiryInMinutes"].ToString()));
            var claims = new[]
            {
                new Claim(username,password),
            };
            var token = new JwtSecurityToken(_configuration["JwtIssuer"], _configuration["JwtAudience"], claims, null, expiry, creds);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

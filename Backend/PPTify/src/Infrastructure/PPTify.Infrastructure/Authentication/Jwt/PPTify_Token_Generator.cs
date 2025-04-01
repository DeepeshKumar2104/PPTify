using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PPTify.Infrastructure.Authentication.Jwt
{
    public class PPTify_Token_Generator
    {
        private readonly IConfiguration configuration;

        public PPTify_Token_Generator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public  string GenerateToken(string username,string Email,string Role)
        {

            var jwtsettings = configuration.GetSection("JwtSettings");
            var issuer = jwtsettings["issuer"];
            var audience = jwtsettings["audience"];
            var secretKey = jwtsettings["secretkey"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //    var claims = new[]
            //    {
            //    new Claim(JwtRegisteredClaimNames.Sub, username),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim("role", "Admin"),
            //    new Claim(ClaimTypes.Role,)
            //};
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Role,Role)

        };
            var token = new JwtSecurityToken(
                issuer: "PPTify",
                audience: "PPTify",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

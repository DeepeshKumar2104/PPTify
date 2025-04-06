using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PPTify.Infrastructure.Authentication.Jwt
{
    public class PPTify_Token_Generator
    {
        private readonly IConfiguration configuration;

        public PPTify_Token_Generator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateTokken(Users user)
        {
            var jwtsettings = configuration.GetSection("JwtSettings");
            var issuer = jwtsettings["Issuer"];
            var audience = jwtsettings["Audience"];
            var security = jwtsettings["Secret"];


            if(string.IsNullOrEmpty(security))
            {
                throw new Exception("Secret Key is missing ");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(security));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            // pay load

            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier,user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokken = new JwtSecurityToken 
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(tokken);

        }
    }
    
}

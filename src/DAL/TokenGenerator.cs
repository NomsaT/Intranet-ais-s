using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DAL
{
    public class TokenGenerator
    {
        public static string GenerateToken(List<int> permissions, int UserId)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Directory where the json files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity("Token");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId.ToString()));
            claimsIdentity.AddClaims(permissions.Select(permission => new Claim(ClaimTypes.Role, permission.ToString())));

            var tokeOptions = new JwtSecurityToken(
                claims: claimsIdentity.Claims,
                issuer: "*",
                audience: "*",
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Morales.BookingSystem.Security.Models;

namespace Morales.BookingSystem.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly AuthDbContext _authctx;
        

        public AuthService(IConfiguration configuration,AuthDbContext ctx)
        {
            _configuration = configuration;
            _authctx = ctx;
        }


        public LoginUser IsValidUserInformation(LoginUser user)
        {
            return _authctx.LoginUsers.FirstOrDefault(u => u.UserName.Equals(user.UserName) &&
                                                           u.HashedPassword.Equals(user.HashedPassword));
        }
        
        public string GenerateJwtToken(LoginUser user)
        {
            var userFound = IsValidUserInformation(user);
            if (userFound == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", userFound.Id.ToString()),
                    new Claim("UserName", userFound.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string Hash(string password)
        {
            //Todo IMPLEMENT PETER! ELLERS KOMMER DER MYRER I BUKSERNE
            return password;
        }

        public List<Permission> GetPermissions(int userId)
        {
            return _authctx.UserPermissions
                .Include(up => up.Permission)
                .Where(up => up.UserID == userId)
                .Select(up => up.Permission)
                .ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Core.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
                                                           u.HashedPassword.Equals(Hash(user)));
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

        //Hashing method used ONLY for logging in a preexisting user 
        public string Hash(LoginUser user)
        {
            var userFromDb = _authctx.LoginUsers.FirstOrDefault(u => u.UserName.Equals(user.UserName));
            byte[] saltedByte = Encoding.ASCII.GetBytes(userFromDb.Salt);
            user.HashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.HashedPassword,
                salt: saltedByte,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return user.HashedPassword;
        }
        //Hashing method used when creating a new account
        public string HashNewPassword(string newPassword, string salt)
        {
            byte[] saltedByte = Encoding.ASCII.GetBytes(salt);
            var hashedNewPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: newPassword,
                salt: saltedByte,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashedNewPassword;
        }

        public List<Permission> GetPermissions(int userId)
        {
            return _authctx.UserPermissions
                .Include(up => up.Permission)
                .Where(up => up.UserID == userId)
                .Select(up => up.Permission)
                .ToList();
        }

        public string CreateNewAccount(LoginUser newUser, Account userAccount)
        {
            var generatedSalt = GenerateSalt();
            var hashedNewPassword = HashNewPassword(newUser.HashedPassword, generatedSalt);
            var newAccount = _authctx.Add(new LoginUser
            {
                UserName = newUser.UserName,
                HashedPassword = hashedNewPassword,
                Salt = generatedSalt,
                AccountId = userAccount.Id
            }).Entity;
            _authctx.SaveChanges();
            return "Success!";
        }

        public string GenerateSalt()
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
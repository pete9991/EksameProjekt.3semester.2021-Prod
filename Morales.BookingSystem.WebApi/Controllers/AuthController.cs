using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morales.BookingSystem.Dtos.Auth;
using Morales.BookingSystem.PolicyHandlers;
using Morales.BookingSystem.Security;
using Morales.BookingSystem.Security.Models;

namespace Morales.BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult<TokenDto> Login([FromBody] LoginDto dto)
        {
            var tokenString = _authService.GenerateJwtToken(new LoginUser
            {
                UserName = dto.UserName,
                HashedPassword = _authService.Hash(new LoginUser
                {
                    UserName = dto.UserName,
                    HashedPassword = dto.Password
                })
            });
            if (string.IsNullOrEmpty(tokenString))
            {
                return BadRequest("Username or Password is invalid");
            }

            return new TokenDto
            {
                Jwt = tokenString,
                Message = "Success"
            };
        }

        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpGet(nameof(GetProfile))]
        public ActionResult<ProfileDto> GetProfile()
        {
            var user = HttpContext.Items["LoginUser"] as LoginUser;
            if (user != null)
            {
                List<Permission> permissions = _authService.GetPermissions(user.Id);
                return Ok(new ProfileDto
                {
                    Permission = permissions.Select(p => p.Name).ToList(),
                    Name = user.UserName
                });
            }

            return Unauthorized();
        }
        

    }
}
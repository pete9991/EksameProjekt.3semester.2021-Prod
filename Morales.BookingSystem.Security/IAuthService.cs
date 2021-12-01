using System.Collections.Generic;
using Morales.BookingSystem.Security.Models;

namespace Morales.BookingSystem.Security
{
    public interface IAuthService
    {
        string GenerateJwtToken(LoginUser userUserName);
        string Hash(string password);
        List<Permission> GetPermissions(int userId);
    }
}
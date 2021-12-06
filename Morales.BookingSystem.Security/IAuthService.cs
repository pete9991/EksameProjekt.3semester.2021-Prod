using System.Collections.Generic;
using Core.Models;
using Morales.BookingSystem.Security.Models;

namespace Morales.BookingSystem.Security
{
    public interface IAuthService
    {
        string GenerateJwtToken(LoginUser userUserName);
        string Hash(LoginUser user);
        List<Permission> GetPermissions(int userId);
        string CreateNewAccount(LoginUser newUser, Account userAccount);
        void EstablishPermissions(Account newAccount);
        LoginUser GetUserInfo(string username);
    }
}
using System.Collections.Generic;

namespace Morales.BookingSystem.Security.Models
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public List<Permission> Permissions { get; set; }
        public int AccountId { get; set; }
    }
    
}
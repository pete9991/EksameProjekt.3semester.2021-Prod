using System.Collections.Generic;

namespace Morales.BookingSystem.Dtos.Auth
{
    public class TokenDto
    {
        public string Jwt { get; set; }
        public string Message { get; set; }
        public List<string> Permission { get; set; }
        public int AccountId { get; set; }
    }
}
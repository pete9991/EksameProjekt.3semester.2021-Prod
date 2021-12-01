

using System.Collections.Generic;

namespace Morales.BookingSystem.Dtos.Auth
{
    public class ProfileDto
    {
        public List<string> Permission { get; set; }
        public string Name { get; set; }
    }
}
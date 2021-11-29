using System.Collections.Generic;

namespace Morales.BookingSystem.EntityFramework.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public List<AppointmentEntity> Appointments { get; set; }
    }
}
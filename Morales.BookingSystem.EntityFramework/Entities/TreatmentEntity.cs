using System;

namespace Morales.BookingSystem.EntityFramework.Entities
{
    public class TreatmentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string  Sex { get; set; }
        public TimeSpan Duration { get; set; }
        public double Price { get; set; }
        public int AppointmentId { get; set; }
        public AppointmentEntity Appointment { get; set; }
    }
}
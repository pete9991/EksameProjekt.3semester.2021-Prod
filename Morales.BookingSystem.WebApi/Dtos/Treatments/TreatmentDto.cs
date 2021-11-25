using System;

namespace Morales.BookingSystem.Dtos.Treatments
{
    public class TreatmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
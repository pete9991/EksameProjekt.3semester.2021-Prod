using System.Collections.Generic;
using Morales.BookingSystem.Dtos.Treatments;

namespace Morales.BookingSystem.Dtos.Appointments
{
    public class AppointmentUpdateDto
    {
        public int Customerid { get; set; }
        public int Employeeid { get; set; }
        public string Date { get; set; }
        public List<TreatmentDto> TreatmentsList { get; set; }
        public int appointmentId { get; set; }
    }
}
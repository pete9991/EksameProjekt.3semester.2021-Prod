using System;
using System.Collections.Generic;

namespace Morales.BookingSystem.Dtos.Appointments
{
    public class AppointmentCreationDto
    {
        public int Customerid { get; set; }
        public int Employeeid { get; set; }
        public DateTime Date { get; set; }
        public List<Core.Models.Treatments> TreatmentsList { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Morales.BookingSystem.Dtos.Treatments;

namespace Morales.BookingSystem.Dtos.Appointments
{
    public class AppointmentCreationDto
    {
        public int Customerid { get; set; }
        public int Employeeid { get; set; }
        public string Date { get; set; }
        public List<TreatmentDto> TreatmentsList { get; set; }
    }
}
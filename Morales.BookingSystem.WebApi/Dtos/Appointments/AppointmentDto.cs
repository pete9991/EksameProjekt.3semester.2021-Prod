using System;
using System.Collections.Generic;
using Core.Models;

namespace Morales.BookingSystem.Dtos.Appointments
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int Customerid { get; set; }
        public string CustomerName { get; set; }
        public int Employeeid { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Core.Models.Treatments> TreatmentsList { get; set; }
        public double TotalPrice { get; set; }

        public DateTime AppointmentEnd { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Core.Models;

namespace Morales.BookingSystem.Dtos.Appointments
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int Customerid { get; set; }
        public int Employeeid { get; set; }
        public string Sex { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Core.Models.Treatments> Treatments { get; set; }
        public double TotalPrice { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int Customerid { get; set; }
        public int Employeeid { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Treatments> TreatmentsList { get; set; }
        public double TotalPrice { get; set; }
    }
}
    

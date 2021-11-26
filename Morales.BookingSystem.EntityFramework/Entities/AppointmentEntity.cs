using System;
using System.Collections.Generic;
using Core.Models;

namespace Morales.BookingSystem.EntityFramework.Entities
{
    public class AppointmentEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public string sex { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Treatments> Treatments { get; set; }
        public double TotalPrice { get; set; }
    }
}
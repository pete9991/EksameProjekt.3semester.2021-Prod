using System;
using System.Collections.Generic;
using Core.Models;

namespace Morales.BookingSystem.EntityFramework.Entities
{
    public class AppointmentEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public double TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public AccountEntity Customer { get; set; }
        public int EmployeeId { get; set; }
        public AccountEntity Employee { get; set; }
        public List<TreatmentEntity> TreatmentsList { get; set; }
        
    }
}
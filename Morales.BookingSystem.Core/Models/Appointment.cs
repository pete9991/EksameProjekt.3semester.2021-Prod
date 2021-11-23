using System.Collections.Generic;

namespace Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int Customerid { get; set; }
        public int Employeeid { get; set; }
        public string Sex { get; set; }
        public float Date { get; set; }
        public int Duration { get; set; }
        private List<int> TreatmentsId;
    }
}
    

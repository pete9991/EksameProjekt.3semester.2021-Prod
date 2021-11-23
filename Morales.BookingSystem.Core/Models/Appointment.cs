using System.Collections.Generic;

namespace Core.Models
{
    public class Appointment
    {
        public int id { get; set; }
        public int customerid { get; set; }
        public int employeeid { get; set; }
        public string type { get; set; }
        public float date { get; set; }
        public int duration { get; set; }
        private List<Treatments> GetTreatment;
    }
}
    

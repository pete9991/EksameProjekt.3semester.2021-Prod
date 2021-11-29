using System;

namespace Core.Models
{
    public class Treatments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public TimeSpan Duration { get; set; }
        public double Price { get; set; }
    }
}
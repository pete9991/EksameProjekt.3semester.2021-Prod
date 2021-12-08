namespace Morales.BookingSystem.Dtos.Appointments
{
    public class AppointmentEventDto
    {
        public string SubjectName { get; set; }
        public double StartInMillis { get; set; }
        public int DurationInMinuts { get; set; }
    }
}
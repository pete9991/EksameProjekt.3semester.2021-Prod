using System.Collections.Generic;
using Core.Models;

namespace Core.IServices
{
    public interface IAppointmentService
    {
        List<Appointment> GetAppointments();
        public Appointment ReadById(int i);
        
        bool Create(Appointment appointment);
        
        Appointment UpdateById(int id, string newName);

        bool Delete(int product);  
    }
}
using System.Collections.Generic;
using Core.Models;

namespace Core.IServices
{
    public interface IAppointmentService
    {
        List<Appointment> GetAllAppointments();
        Appointment ReadById(int appointmentId);
        
        Appointment CreateAppointment (Appointment appointmentToCreate);
        
        Appointment UpdateById(int appointmentToUpdateId, Appointment updatedAppointment);

        Appointment DeleteAppointment(int deletedAppointmentId);

        List<Appointment> GetAppointmentsFromHairdresser(int employeeId);
        double CalculateTotalPrice(Appointment appointment);
    }
}
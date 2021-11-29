using System;
using System.Collections.Generic;
using System.Threading;
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

        TimeSpan CalculateDuration(Appointment appointment);
    }
}
using System.Collections.Generic;
using Core.Models;

namespace Morales.BookingSystem.Domain.IRepositories
{
    public interface IAppointmentRepository
    {
        List<Appointment> readAllAppointments();
        Appointment ReadById(int appointmentId);
        Appointment CreateAppointment(Appointment appointmentToCreate);
        Appointment UpdateById(int appointmentToUpdateId, Appointment updatedAppointment);
        List<Appointment> GetAppointmentFromHairdresser(int employeeId);
        Appointment DeleteAppointment(int deletedAppointmentId);
        List<Appointment> GetAppointmentFromUser(int userId);
    }
}
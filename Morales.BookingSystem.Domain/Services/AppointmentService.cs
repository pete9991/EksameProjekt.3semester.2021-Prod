using System.Collections.Generic;
using System.IO;
using Core.IServices;
using Core.Models;
using Morales.BookingSystem.Domain.IRepositories;

namespace Morales.BookingSystem.Domain.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            if (appointmentRepository == null)
            {
                throw new InvalidDataException("A AppointmentService need an a appointmentRepository");
            }

            _appointmentRepository = appointmentRepository;
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.readAllAppointments();
        }

        public Appointment ReadById(int appointmentId)
        {
            return _appointmentRepository.ReadById(appointmentId);
        }

        public bool CreateAppointment(Appointment appointmentToCreate)
        {
            return _appointmentRepository.CreateAppointment(appointmentToCreate);
        }

        public Appointment UpdateById(int appointmentToUpdateId, Appointment updatedAppointment)
        {
            return _appointmentRepository.UpdateById(appointmentToUpdateId, updatedAppointment);
        }

        public bool DeleteAppointment(int deletedAppointmentId)
        {
            throw new System.NotImplementedException();
        }
        
        
        public List<Appointment> GetAppointmentsFromHairdresser(int employeeId)
        {
            return _appointmentRepository.GetAppointmentFromHairdresser();
        }
    }
}
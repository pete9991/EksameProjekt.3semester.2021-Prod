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
                throw new InvalidDataException("A AppointmentService need an a  appointmentRepository");
            }

            _appointmentRepository = appointmentRepository;
        }

        public List<Appointment> GetAllAppointments()
        {
            throw new System.NotImplementedException();
        }

        public Appointment ReadById(int appointmentId)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateAppointment(Appointment appointmentToCreate)
        {
            throw new System.NotImplementedException();
        }

        public Appointment UpdateById(int appointmentToUpdateId, Appointment updatedAppointment)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteAppointment(int deletedAppointmentId)
        {
            throw new System.NotImplementedException();
        }

        public List<Appointment> GetAppointmentsFromHairdresser(int employeeId)
        {
            throw new System.NotImplementedException();
        }
    }
}
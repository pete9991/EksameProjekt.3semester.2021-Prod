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
                throw new InvalidDataException("An AppointmentService need an appointmentRepository");
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

        public Appointment CreateAppointment(Appointment appointmentToCreate)
        {
            return _appointmentRepository.CreateAppointment(appointmentToCreate);
        }

        public Appointment UpdateById(int appointmentToUpdateId, Appointment updatedAppointment)
        {
            return _appointmentRepository.UpdateById(appointmentToUpdateId, updatedAppointment);
        }

        public Appointment DeleteAppointment(int deletedAppointmentId)
        {
            return _appointmentRepository.DeleteAppointment(deletedAppointmentId);
        }
        
        
        public List<Appointment> GetAppointmentsFromHairdresser(int employeeId)
        {
            return _appointmentRepository.GetAppointmentFromHairdresser(employeeId);
        }

        public double CalculateTotalPrice(Appointment appointment)
        {
            double calculatedPrice = 0;
            foreach (var treatment in appointment.Treatments)
            {
               calculatedPrice = calculatedPrice + treatment.Price;
            }
            return calculatedPrice;
        }
    }
}
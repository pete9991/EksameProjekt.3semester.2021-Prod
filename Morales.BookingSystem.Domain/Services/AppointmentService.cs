using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
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
            appointmentToCreate.TotalPrice = CalculateTotalPrice(appointmentToCreate);
            return _appointmentRepository.CreateAppointment(appointmentToCreate);
        }

        public Appointment UpdateById(int appointmentToUpdateId, Appointment updatedAppointment)
        {
            updatedAppointment.TotalPrice = CalculateTotalPrice(updatedAppointment);
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
            foreach (var treatment in appointment.TreatmentsList)
            {
               calculatedPrice = calculatedPrice + treatment.Price;
            }
            return calculatedPrice;
        }
        
        public TimeSpan CalculateDuration(Appointment appointment)
        {
            var TotalDuration = new TimeSpan(0, 0, 0);
            foreach (var treatment in appointment.TreatmentsList)
            {
                TotalDuration = TotalDuration + treatment.Duration;
            }

            return TotalDuration;
        }

        public DateTime CalculateAppointmentEnd(Appointment appointment)
        {
            var EndTime = new DateTime();
            EndTime = appointment.Date.Add(appointment.Duration);
            appointment.AppointmentEnd = EndTime;
            
            return appointment.AppointmentEnd;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Morales.BookingSystem.Domain.IRepositories;
using Morales.BookingSystem.Domain.Services;
using Morales.BookingSystem.EntityFramework.Entities;

namespace Morales.BookingSystem.EntityFramework.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly MainDbContext _ctx;

        public AppointmentRepository(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<Appointment> readAllAppointments()
        {
            return _ctx.Appointments
                .Select(ae => new Appointment
                {
                    Id = ae.Id,
                    Customerid = ae.CustomerId,
                    Employeeid = ae.EmployeeId,
                    Sex = ae.sex,
                    Date = ae.Date,
                    Duration = ae.Duration,
                    Treatments = ae.Treatments
                })
                .ToList();
        }

        public Appointment ReadById(int appointmentId)
        {
            return _ctx.Appointments.Where(ae => ae.Id == appointmentId)
                .Select(ae => new Appointment
                {
                    Id = ae.Id,
                    Customerid = ae.CustomerId,
                    Employeeid = ae.EmployeeId,
                    Sex = ae.sex,
                    Date = ae.Date,
                    Duration = ae.Duration,
                    Treatments = ae.Treatments
                })
                .FirstOrDefault(ae => ae.Id == appointmentId);
        }

        public bool CreateAppointment(Appointment appointmentToCreate)
        {
            var entity = _ctx.Add(new AppointmentEntity()
            {
                CustomerId = appointmentToCreate.Customerid,
                EmployeeId = appointmentToCreate.Employeeid,
                sex = appointmentToCreate.Sex,
                Date = appointmentToCreate.Date,
                Duration = appointmentToCreate.Duration,
                Treatments = appointmentToCreate.Treatments

            }).Entity;
            _ctx.SaveChanges();
            return true;
        }

        public Appointment UpdateById(int appointmentToUpdateId, Appointment updatedAppointment)
        {
            var previousAppointment = _ctx.Appointments.Where(ae => ae.Id == appointmentToUpdateId)
                .Select(ae => new Appointment
                {
                    Id = ae.Id,
                    Customerid = ae.CustomerId,
                    Employeeid = ae.EmployeeId,
                    Sex = ae.sex,
                    Date = ae.Date,
                    Duration = ae.Duration,
                    Treatments = ae.Treatments
                })
                .FirstOrDefault(ae => ae.Id == appointmentToUpdateId);

            var appointmentEntity = new AppointmentEntity()
            {
                Id = previousAppointment.Id,
                CustomerId = previousAppointment.Customerid,
                EmployeeId = updatedAppointment.Employeeid,
                sex = previousAppointment.Sex,
                Date = updatedAppointment.Date,
                Duration = updatedAppointment.Duration,
                Treatments = updatedAppointment.Treatments
            };
            var entity = _ctx.Update(appointmentEntity).Entity;
            _ctx.SaveChanges();
            return new Appointment
            {
                Id = previousAppointment.Id,
                Customerid = previousAppointment.Customerid,
                Employeeid = updatedAppointment.Employeeid,
                Sex = previousAppointment.Sex,
                Date = updatedAppointment.Date,
                Duration = updatedAppointment.Duration,
                Treatments = updatedAppointment.Treatments
            };

        }

        public List<Appointment> GetAppointmentFromHairdresser(int employeeId)
        {
            return _ctx.Appointments.Where(ae => ae.EmployeeId== employeeId)
                .Select(ae => new Appointment
                {
                    Id = ae.Id,
                    Customerid = ae.CustomerId,
                    Employeeid = ae.EmployeeId,
                    Sex = ae.sex,
                    Date = ae.Date,
                    Duration = ae.Duration,
                    Treatments = ae.Treatments
                })
                .ToList();
        }

        public bool DeleteAppointment(int deletedAppointmentId)
        {
            _ctx.Appointments.Remove(new AppointmentEntity() {Id = deletedAppointmentId});
            _ctx.SaveChanges();
            return true;
        }
    }
}
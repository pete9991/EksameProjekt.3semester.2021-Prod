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
                    Treatments = ae.Treatments,
                    TotalPrice = ae.TotalPrice
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
                    Treatments = ae.Treatments,
                    TotalPrice = ae.TotalPrice
                })
                .FirstOrDefault(ae => ae.Id == appointmentId);
        }

        public Appointment CreateAppointment(Appointment appointmentToCreate)
        {
            var entity = _ctx.Add(new AppointmentEntity()
            {
                CustomerId = appointmentToCreate.Customerid,
                EmployeeId = appointmentToCreate.Employeeid,
                sex = appointmentToCreate.Sex,
                Date = appointmentToCreate.Date,
                Duration = appointmentToCreate.Duration,
                Treatments = appointmentToCreate.Treatments,
                TotalPrice = appointmentToCreate.TotalPrice
            }).Entity;
            _ctx.SaveChanges();
            return new Appointment()
            {
                Id = entity.Id,
                Customerid = entity.CustomerId,
                Employeeid = entity.EmployeeId,
                Sex = entity.sex,
                Date = entity.Date,
                Duration = entity.Duration,
                Treatments = entity.Treatments
            } ;
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
                    Treatments = ae.Treatments,
                    TotalPrice = ae.TotalPrice
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
                Treatments = updatedAppointment.Treatments,
                TotalPrice = updatedAppointment.TotalPrice
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
                Treatments = updatedAppointment.Treatments,
                TotalPrice = updatedAppointment.TotalPrice
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
                    Treatments = ae.Treatments,
                    TotalPrice = ae.TotalPrice
                })
                .ToList();
        }

        public Appointment DeleteAppointment(int deletedAppointmentId)
        {
            var appointmentToDelete = _ctx.Appointments
                .Select(ae => new Appointment
                {
                    Id = ae.Id,
                    Customerid = ae.CustomerId,
                    Employeeid = ae.EmployeeId,
                    Sex = ae.sex,
                    Date = ae.Date,
                    Duration = ae.Duration,
                    Treatments = ae.Treatments,
                    TotalPrice = ae.TotalPrice
                })
                .FirstOrDefault(a => a.Id == deletedAppointmentId);
            _ctx.Appointments.Remove(new AppointmentEntity() {Id = deletedAppointmentId});
            _ctx.SaveChanges();
            return appointmentToDelete;
        }
    }
}
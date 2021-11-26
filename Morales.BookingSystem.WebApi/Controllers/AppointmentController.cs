using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Morales.BookingSystem.Dtos.Appointments;

namespace Morales.BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _AppointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _AppointmentService = appointmentService;
        }

        [HttpGet]
        public ActionResult<AppointmentsDto> GetAll()
        {
            try
            {
                var appointments = _AppointmentService.GetAllAppointments()
                    .Select(a => new AppointmentDto()
                    {
                        Id = a.Id,
                        Customerid = a.Customerid,
                        Employeeid = a.Employeeid,
                        Date = a.Date,
                        Duration = a.Duration,
                        Sex = a.Sex,
                        TreatmentsList = a.TreatmentsList,
                        TotalPrice = a.TotalPrice
                    })
                    .ToList();
                return Ok(new AppointmentsDto
                {
                    AppointmentList = appointments
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{AppointmentId:int}")]
        public ActionResult<AppointmentDto> GetAppointment(int AppointmentId)
        {
            var appointment = _AppointmentService.ReadById(AppointmentId);
            var dto = new AppointmentDto
            {
                Id = appointment.Id,
                Customerid = appointment.Customerid,
                Employeeid = appointment.Employeeid,
                Sex = appointment.Sex,
                Date = appointment.Date,
                Duration = appointment.Duration,
                TreatmentsList = appointment.TreatmentsList,
                TotalPrice = appointment.TotalPrice
            };
            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<AppointmentDto> CreateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            var appointmentToCreate = new Appointment()
            {
                Customerid = appointmentDto.Customerid,
                Employeeid = appointmentDto.Employeeid,
                Sex = appointmentDto.Sex,
                Date = appointmentDto.Date,
                Duration = appointmentDto.Duration,
                TreatmentsList = appointmentDto.TreatmentsList,
                TotalPrice = appointmentDto.TotalPrice
            };
            var appointmentCreated = _AppointmentService.CreateAppointment(appointmentToCreate);
            return Created($"https//localhost/api/appointment/{appointmentToCreate.Id}", appointmentCreated);
        }

        [HttpPut("{id:int}")]
        public ActionResult<AppointmentsDto> UpdateAppointment(int id, [FromBody] AppointmentDto appointmentToUpdate)
        {
            return Ok(_AppointmentService.UpdateById(id, new Appointment()
            {
                Employeeid = appointmentToUpdate.Employeeid,
                Date = appointmentToUpdate.Date,
                Duration = appointmentToUpdate.Duration,
                TreatmentsList = appointmentToUpdate.TreatmentsList,
            }));
        }

        [HttpDelete("{id:int}")]
        public ActionResult<AppointmentDto> DeleteAppointment(int id)
        {
            var appointment= _AppointmentService.DeleteAppointment(id);
            var dto = new AppointmentDto
            {
                Id = appointment.Id,
                Customerid = appointment.Customerid,
                Employeeid = appointment.Employeeid,
                Sex = appointment.Sex,
                Date = appointment.Date,
                Duration = appointment.Duration,
                TreatmentsList = appointment.TreatmentsList,
                TotalPrice = appointment.TotalPrice
            };
            return Ok(dto);
        }


        [HttpGet("hairdresser/{employeeid:int}")]
        public ActionResult<AppointmentsDto> GetAppointmentFromHairdresser(int employeeid)
        {
            var appointment = _AppointmentService.GetAppointmentsFromHairdresser(employeeid)
                .Select(appointment => new AppointmentDto()
                {
                    Id = appointment.Id,
                    Customerid = appointment.Customerid,
                    Employeeid = appointment.Employeeid,
                    Sex = appointment.Sex,
                    Date = appointment.Date,
                    Duration = appointment.Duration,
                    TreatmentsList = appointment.TreatmentsList,
                    TotalPrice = appointment.TotalPrice
                })
                .ToList();
            return Ok(new AppointmentsDto
            {
                AppointmentList = appointment
            });
            
        }
    }
}
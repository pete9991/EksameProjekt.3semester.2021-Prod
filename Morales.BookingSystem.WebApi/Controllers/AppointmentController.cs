using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Morales.BookingSystem.Dtos.Appointments;
using Morales.BookingSystem.PolicyHandlers;

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

        [Authorize(Policy = nameof(EmployeeHandler))]
        [HttpGet]
        public ActionResult<AppointmentsDto> GetAll()
        {
            try
            {
                var appointments = _AppointmentService.GetAllAppointments()
                    .Select(a => new AppointmentDto()
                    {
                        Id = a.Id,
                        Customerid = a.Customer.Id,
                        CustomerName = a.Customer.Name,
                        Employeeid = a.Employee.Id,
                        EmployeeName = a.Employee.Name,
                        Date = a.Date,
                        Duration = a.Duration,
                        TreatmentsList = a.TreatmentsList,
                        TotalPrice = a.TotalPrice,
                        AppointmentEnd = a.AppointmentEnd
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

        [Authorize(Policy = nameof(EmployeeHandler))]
        [HttpGet("employee/events")]
        public ActionResult<AppointmentsDto> GetAllAppointmentEvents()
        {
            try
            {
                var appointments = _AppointmentService.GetAllAppointments()
                    .Select(a => new AppointmentEventDto()
                    {
                        SubjectName = a.Customer.Name,
                        StartInMillis = a.Date
                            .ToUniversalTime().Subtract(new DateTime(1970,1,1,0,0,0))
                            .TotalMilliseconds,
                        DurationInMinuts = (int)a.Duration.TotalMinutes
                    })
                    .ToList();
                return Ok(new AppointmentEventsDto
                {
                    AppointmentEvents = appointments
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpGet("employee/events/user")]
        public ActionResult<AppointmentsDto> GetAllHairdresserEvents()
        {
            try
            {
                var appointments = _AppointmentService.GetAllAppointments()
                    .Select(a => new AppointmentEventDto()
                    {
                        SubjectName = a.Employee.Name,
                        StartInMillis = a.Date
                            .ToUniversalTime().Subtract(new DateTime(1970,1,1,0,0,0))
                            .TotalMilliseconds,
                        DurationInMinuts = (int)a.Duration.TotalMinutes
                    })
                    .ToList();
                return Ok(new AppointmentEventsDto
                {
                    AppointmentEvents = appointments
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpGet("{AppointmentId:int}")]
        public ActionResult<AppointmentDto> GetAppointment(int AppointmentId)
        {
            var appointment = _AppointmentService.ReadById(AppointmentId);
            var dto = new AppointmentDto
            {
                Id = appointment.Id,
                Customerid = appointment.Customerid,
                Employeeid = appointment.Employeeid,
                Date = appointment.Date,
                Duration = appointment.Duration,
                TreatmentsList = appointment.TreatmentsList,
                TotalPrice = appointment.TotalPrice,
                AppointmentEnd = appointment.AppointmentEnd
            };
            return Ok(dto);
        }

        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpPost]
        public ActionResult<AppointmentCreationDto> CreateAppointment([FromBody] AppointmentCreationDto appointmentDto)
        {
            var appointmentToCreate = new Appointment()
            {
                Customerid = appointmentDto.Customerid,
                Employeeid = appointmentDto.Employeeid,
                Date = DateTime.ParseExact(appointmentDto.Date, "yyyy-MM-dd HH:mm:ss",CultureInfo.CurrentCulture),
                TreatmentsList = appointmentDto.TreatmentsList.Select(tDto => new Treatments()
                {
                    Id = tDto.Id,
                    Duration = TimeSpan.FromMinutes(tDto.Duration.minutes),
                    Name = tDto.Name,
                    Price = tDto.Price,
                    Sex = tDto.Sex
                }).ToList()
            };
            var appointmentReturn = new AppointmentCreationDto()
            {
                Customerid = appointmentDto.Customerid,
                Employeeid = appointmentDto.Employeeid,
                Date = appointmentDto.Date,
                TreatmentsList = appointmentDto.TreatmentsList
            };
            _AppointmentService.CreateAppointment(appointmentToCreate);
            return Created($"https//localhost/api/appointment/{appointmentToCreate.Id}", appointmentReturn);
        }

        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpPut("{id:int}")]
        public ActionResult<AppointmentsDto> UpdateAppointment([FromBody] AppointmentUpdateDto appointmentToUpdate)
        {
            var id = appointmentToUpdate.appointmentId;
            return Ok(_AppointmentService.UpdateById(id, new Appointment()
            {
                Employeeid = appointmentToUpdate.Employeeid,
                Date = DateTime.ParseExact(appointmentToUpdate.Date, "yyyy-MM-dd HH:mm:ss",CultureInfo.CurrentCulture),
                TreatmentsList = appointmentToUpdate.TreatmentsList.Select(tDto => new Treatments()
                {
                    Id = tDto.Id,
                    Duration = TimeSpan.FromMinutes(tDto.Duration.minutes),
                    Name = tDto.Name,
                    Price = tDto.Price,
                    Sex = tDto.Sex
                }).ToList()
            }));
        }

        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpDelete("{id:int}")]
        public ActionResult<AppointmentDto> DeleteAppointment(int id)
        {
            var appointment= _AppointmentService.DeleteAppointment(id);
            var dto = new AppointmentDto
            {
                Id = appointment.Id,
                Customerid = appointment.Customerid,
                Employeeid = appointment.Employeeid,
                Date = appointment.Date,
                Duration = appointment.Duration,
                TreatmentsList = appointment.TreatmentsList,
                TotalPrice = appointment.TotalPrice,
                AppointmentEnd = appointment.AppointmentEnd
            };
            return Ok(dto);
        }
        
        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpGet("hairdresser/{employeeid:int}")]
        public ActionResult<AppointmentsDto> GetAppointmentFromHairdresser(int employeeid)
        {
            var appointment = _AppointmentService.GetAppointmentsFromHairdresser(employeeid)
                .Select(appointment => new AppointmentDto()
                {
                    Id = appointment.Id,
                    Customerid = appointment.Customerid,
                    Employeeid = appointment.Employeeid,
                    Date = appointment.Date,
                    Duration = appointment.Duration,
                    TreatmentsList = appointment.TreatmentsList,
                    TotalPrice = appointment.TotalPrice,
                    AppointmentEnd = appointment.AppointmentEnd
                })
                .ToList();
            return Ok(new AppointmentsDto
            {
                AppointmentList = appointment
            });
            
        }

        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpGet("user/{userid:int}")]
        public ActionResult<AppointmentsDto> GetAppointmentsFromUser(int userid)
            {
                var appointment = _AppointmentService.GetAppointmentsFromUser(userid)
                    .Select(appointment => new AppointmentDto()
                    {
                        Id = appointment.Id,
                        Customerid = appointment.Customerid,
                        Employeeid = appointment.Employeeid,
                        EmployeeName = appointment.Employee.Name,
                        Date = appointment.Date,
                        Duration = appointment.Duration,
                        TreatmentsList = appointment.TreatmentsList,
                        TotalPrice = appointment.TotalPrice,
                        AppointmentEnd = appointment.AppointmentEnd
                    })
                    .ToList();
                return Ok(new AppointmentsDto
                {
                    AppointmentList = appointment
                });
                
            }
        
        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpGet("user/events/{userid:int}")]
        public ActionResult<AppointmentsDto> GetAppointmentEventsFromUser(int userid)
        {
            var appointment = _AppointmentService.GetAppointmentsFromUser(userid)
                .Select(a => new AppointmentEventDto()
                {
                    SubjectName = a.Employee.Name,
                    StartInMillis = (long)a.Date
                        .ToUniversalTime().Subtract(new DateTime(1970,1,1,0,0,0))
                        .TotalMilliseconds,
                    DurationInMinuts = (int)a.Duration.TotalMinutes
                })
                .ToList();
            return Ok(new AppointmentEventsDto
            {
                AppointmentEvents = appointment
            });
                
        }
        
    }
}
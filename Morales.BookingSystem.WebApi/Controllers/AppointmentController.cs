using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                        Treatments = a.Treatments
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
    }
}
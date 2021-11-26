using System;
using System.Linq;
using Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Morales.BookingSystem.Dtos.Treatments;
using Morales.BookingSystem.EntityFramework.Entities;

namespace Morales.BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        private readonly ITreatmentService _treatmentService;

        public TreatmentController(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }

        [HttpGet]
        public ActionResult<TreatmentsDto> GetAll()
        {
            try
            {
                var treatments = _treatmentService.GetAll()
                    .Select(t => new TreatmentDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Duration = t.Duration,
                        Price = t.Price
                    }).ToList();
                return Ok(new TreatmentsDto
                {
                    TreatmentList = treatments
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
using System;
using System.Linq;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morales.BookingSystem.Dtos.Treatments;
using Morales.BookingSystem.EntityFramework.Entities;
using Morales.BookingSystem.PolicyHandlers;

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

        [AllowAnonymous]
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
                        Duration = new TimeSpanDto()
                        {
                            ticks = t.Duration.Ticks,
                            days = t.Duration.Days,
                            hours = t.Duration.Hours,
                            milliseconds = t.Duration.Milliseconds,
                            minutes = t.Duration.Minutes,
                            seconds = t.Duration.Seconds,
                            totalDays = t.Duration.TotalDays,
                            totalHours = t.Duration.TotalHours,
                            totalMilliseconds = t.Duration.TotalMilliseconds,
                            totalMinutes = t.Duration.TotalMinutes,
                            totalSeconds = t.Duration.TotalSeconds
                        },
                        Price = t.Price
                    }).ToList();
                return Ok(new TreatmentsDto
                {
                    TreatmentsList = treatments
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpGet("{TreatmentId:int}")]
        public ActionResult<TreatmentDto> GetTreatment(int TreatmentId)
        {
            var treatment = _treatmentService.GetTreatment(TreatmentId);
            var dto = new TreatmentDto
            {
                Id = treatment.Id,
                Name = treatment.Name,
                Duration = new TimeSpanDto()
                {
                    minutes = treatment.Duration.Minutes
                },
                Price = treatment.Price
            };
            return Ok(dto);
        }

        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpGet("{sex:alpha}")]
        public ActionResult<TreatmentsDto> GetTreatmentBySex(string sex)
        {
            var treatments = _treatmentService.GetTreatmentsBySex(sex)
                .Select(treatments => new TreatmentDto()
                {
                    Id = treatments.Id,
                    Name = treatments.Name,
                    Duration = new TimeSpanDto()
                    {
                        minutes = treatments.Duration.Minutes
                    },
                    Price = treatments.Price,
                    Sex = treatments.Sex
                })
                .ToList();
            return Ok(new TreatmentsDto
            {
                TreatmentsList = treatments
            });
        }

        [Authorize(Policy = nameof(AdminHandler))]
        [HttpPost]
        public ActionResult<TreatmentsDto> CreateTreatment([FromBody] TreatmentDto treatmentDto)
        {
            var treatmentToCreate = new Treatments()
            {
                Name = treatmentDto.Name,
                Duration = TimeSpan.FromMinutes(treatmentDto.Duration.minutes),
                Price = treatmentDto.Price
            };
            var treatmentCreated = _treatmentService.CreateTreatment(treatmentToCreate);
            return Created($"https://localhost/api/Treatment/{treatmentCreated.Id}", treatmentCreated);
        }

        [Authorize(Policy = nameof(AdminHandler))]
        [HttpPut("{id:int}")]
        public ActionResult<TreatmentsDto> UpdateTreatment(int id, [FromBody] TreatmentDto treatmentToUpdate)
        {
            return Ok(_treatmentService.UpdateTreatment(new Treatments()
                {
                    Name = treatmentToUpdate.Name,
                    Duration = TimeSpan.FromMinutes(treatmentToUpdate.Duration.minutes),
                    Price = treatmentToUpdate.Price
                }
            ));
        }

        [Authorize(Policy = nameof(AdminHandler))]
        [HttpDelete("{id:int}")]
        public ActionResult<TreatmentDto> DeleteTreatment(int id)
        {
            var treatment = _treatmentService.DeleteTreatment(id);
            var dto = new TreatmentDto
            {
                Id = treatment.Id,
                Name = treatment.Name,
                Duration = new TimeSpanDto()
                {
                    minutes = treatment.Duration.Minutes
                },
                Price = treatment.Price
            };

            return Ok(dto);
        }
    }
    
}
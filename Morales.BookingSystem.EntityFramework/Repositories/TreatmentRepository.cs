using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Morales.BookingSystem.Domain.IRepositories;
using Morales.BookingSystem.EntityFramework.Entities;

namespace Morales.BookingSystem.EntityFramework.Repositories
{
    public class TreatmentRepository : ITreatmentRepository

    {
        private readonly MainDbContext _ctx;

        public TreatmentRepository(MainDbContext ctx)
        {
            _ctx = ctx;
        }
        public List<Treatments> GetAll()
        {
            return _ctx.Treatments.Select(te => new Treatments
                {
                    Id = te.Id,
                    Name = te.Name,
                    Duration = te.Duration,
                    Price = te.Price
                })
                .ToList();
        }

        public Treatments GetTreatment(int treatmentId)
        {
            return _ctx.Treatments.Where(te => te.Id == treatmentId)
                .Select(te => new Treatments
                {
                    Id = te.Id,
                    Name = te.Name,
                    Duration = te.Duration,
                    Price = te.Price
                })
                .FirstOrDefault(te => te.Id == treatmentId);
        }

        public List<Treatments> GetTreatmentBySex()
        {
            throw new System.NotImplementedException();
        }

        public Treatments DeleteTreatment(int treatmentId)
        {
            var treatmentToDelete = GetTreatment(treatmentId);
            _ctx.Treatments.Remove(new TreatmentEntity() {Id = treatmentId});
            _ctx.SaveChanges();
            return treatmentToDelete;
        }

        public Treatments UpdateTreatment(Treatments treatments)
        {
            var unUpdatedTreatment = GetTreatment(treatments.Id);
            var treatmentEntity = new TreatmentEntity
            {
                Id = unUpdatedTreatment.Id,
                Name = treatments.Name,
                Duration = treatments.Duration,
                Price = treatments.Price
            };
            var entity = _ctx.Update(treatmentEntity).Entity;
            _ctx.SaveChanges();
            return GetTreatment(treatments.Id);
        }

        public Treatments CreateTreatment(Treatments treatments)
        {
            var entity = _ctx.Add(new TreatmentEntity
            {
                Name = treatments.Name,
                Duration = treatments.Duration,
                Price = treatments.Price
            }).Entity;
            _ctx.SaveChanges();
            return GetTreatment(entity.Id);
        }
    }
}
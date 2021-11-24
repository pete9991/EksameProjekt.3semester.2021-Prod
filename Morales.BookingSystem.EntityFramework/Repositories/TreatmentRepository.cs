using System.Collections.Generic;
using Core.Models;
using Morales.BookingSystem.Domain.IRepositories;

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
            throw new System.NotImplementedException();
        }

        public Treatments GetTreatment(int treatmentId)
        {
            throw new System.NotImplementedException();
        }

        public Treatments DeleteTreatment(int treatmentId)
        {
            throw new System.NotImplementedException();
        }

        public Treatments UpdateTreatment(Treatments treatments)
        {
            throw new System.NotImplementedException();
        }

        public Treatments CreateTreatment(Treatments treatments)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using Core.IServices;
using Core.Models;
using Morales.BookingSystem.Domain.IRepositories;

namespace Morales.BookingSystem.Domain.Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentRepository _treatmentRepo;

        public TreatmentService(ITreatmentRepository repository)
        {
            _treatmentRepo = repository;
        }

        public List<Treatments> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Treatments GetTreatment(int id)
        {
            throw new System.NotImplementedException();
        }

        public Treatments DeleteTreatment(int id)
        {
            throw new System.NotImplementedException();
        }

        public Treatments CreateTreatment()
        {
            throw new System.NotImplementedException();
        }

        public Treatments UpdateTreatment(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
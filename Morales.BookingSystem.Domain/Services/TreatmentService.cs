using System.Collections.Generic;
using System.IO;
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
            if (repository == null)
            {
                throw new InvalidDataException("TreatmentRepository Cannot Be Null!");
            }
            _treatmentRepo = repository;
        }

        public List<Treatments> GetAll()
        {
            return _treatmentRepo.GetAll();
        }

        public Treatments GetTreatment(int id)
        {
            return _treatmentRepo.GetTreatment(id);
        }

        public Treatments DeleteTreatment(int id)
        {
            return _treatmentRepo.DeleteTreatment(id);
        }

        public Treatments CreateTreatment(Treatments treatments)
        {
            return _treatmentRepo.CreateTreatment(treatments);
        }

        public Treatments UpdateTreatment(Treatments treatments)
        {
            return _treatmentRepo.UpdateTreatment(treatments);
        }
    }
}
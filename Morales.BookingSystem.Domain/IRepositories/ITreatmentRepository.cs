using System.Collections.Generic;
using Core.Models;

namespace Morales.BookingSystem.Domain.IRepositories
{
    public interface ITreatmentRepository
    {
        public List<Treatments> GetAll();
        public Treatments GetTreatment(int treatmentId);
        public List<Treatments> GetTreatmentBySex();
        public Treatments DeleteTreatment(int treatmentId);
        public Treatments UpdateTreatment(Treatments treatments);
        public Treatments CreateTreatment(Treatments treatments);
    }
}
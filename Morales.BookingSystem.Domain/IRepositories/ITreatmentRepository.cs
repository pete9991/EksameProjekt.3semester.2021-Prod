using System.Collections.Generic;
using Core.Models;

namespace Morales.BookingSystem.Domain.IRepositories
{
    public interface ITreatmentRepository
    {
        public List<Treatments> GetAll();
        public Treatments GetTreatment(int treatmentId);
        public Treatments DeleteTreatment(int treatmentId);
        public Treatments UpdateTreatment(int treatmentId);
        public Treatments CreateTreatment(int treatmentId);
    }
}
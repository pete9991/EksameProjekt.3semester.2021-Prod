using System.Collections.Generic;
using Core.Models;

namespace Core.IServices
{
    public interface ITreatmentService
    {
        public List<Treatments> GetAll();
        public Treatments GetTreatment(int id);
        public Treatments DeleteTreatment(int id);
        public Treatments CreateTreatment();
        public Treatments UpdateTreatment(int id);
    }
}
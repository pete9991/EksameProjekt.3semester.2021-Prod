using System.Collections.Generic;
using Core.Models;

namespace Core.IServices
{
    public interface ITreatmentService
    {
        public List<Treatments> GetAll();
        public Treatments GetTreatment(int id);
        public List<Treatments> GetTreatmentsBySex(string sex);
        public Treatments DeleteTreatment(int id);
        public Treatments CreateTreatment(Treatments treatments);
        public Treatments UpdateTreatment(Treatments treatments);
    }
}
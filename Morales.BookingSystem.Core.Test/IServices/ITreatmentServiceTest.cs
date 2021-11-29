using System.Collections.Generic;
using Core.IServices;
using Core.Models;
using Moq;
using Xunit;

namespace Morales.BookingSystem.Core.Test.IServices
{
    public class ITreatmentServiceTest
    {
        [Fact]
        public void ITreatmentService_Exists()
        {
            var serviceMock = new Mock<ITreatmentService>();
            Assert.NotNull(serviceMock.Object);
        }
        #region GetAll Tests
        [Fact]
        public void GetTreatments_WithNoParams_ReturnsListOfProducts()
        {
            var serviceMock = new Mock<ITreatmentService>();
            serviceMock
                .Setup(s => s.GetAll())
                .Returns(new List<Treatments>());
        }
        #endregion

        #region GetTreatmentBySex Test
        
        [Fact]
        public void GetTreatmentBySex_WithNoParams_ReturnSingleTreatment()
        {
            var serviceMock = new Mock<ITreatmentService>();
            var testString = "Male";
            serviceMock
                .Setup(s => s.GetTreatmentsBySex(testString))
                .Returns(new List<Treatments>());
        }
        #endregion
        
        #region GetSingleTreatment Test
        [Fact]
        public void GetTreatment_WithParams_Returns()
        
        {
            var serviceMock = new Mock<ITreatmentService>();
            var treatmentId = 1;
            serviceMock
                .Setup(s => s.GetTreatment(1))
                .Returns(new Treatments());
            Assert.NotNull(serviceMock.Object.GetTreatment(1));
        }
        #endregion
        
        #region DeleteTreatment Test

        [Fact]
        public void DeleteTreatment_WithParams_ReturnsDeletedTreatment()
        {
            var serviceMock = new Mock<ITreatmentService>();
            serviceMock
                .Setup(s => s.DeleteTreatment(1))
                .Returns(new Treatments());
            Assert.NotNull(serviceMock.Object.DeleteTreatment(1));
        }
        #endregion
        
        #region CreateTreatment Test

        [Fact]
        public void CreateTreatment_ReturnsTreatmentCreated()
        {
            var treatment = new Treatments() {Id = 1};
            var serviceMock = new Mock<ITreatmentService>();
            serviceMock
                .Setup(s => s.CreateTreatment(treatment))
                .Returns(new Treatments());
            Assert.NotNull(serviceMock.Object.CreateTreatment(treatment));
        }
        #endregion
        
        #region UpdateTreatment Test
        [Fact]
        public void UpdateTreatment_WithParams_ReturnsUpdatedTreatment()
        {
            var serviceMock = new Mock<ITreatmentService>();
            var treatment = new Treatments(){Id = 1};
            serviceMock
                .Setup(s => s.UpdateTreatment(treatment))
                .Returns(new Treatments());
            Assert.NotNull(serviceMock.Object.UpdateTreatment(treatment));
        }
        #endregion
    }
    
}
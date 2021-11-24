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
        
        #region GetSingleTreatment Test
        [Fact]
        public void GetTreatment_WithParams_ReturnsSingleTreatment()
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
            var serviceMock = new Mock<ITreatmentService>();
            serviceMock
                .Setup(s => s.CreateTreatment())
                .Returns(new Treatments());
            Assert.NotNull(serviceMock.Object.CreateTreatment());
        }
        #endregion
        
        #region UpdateTreatment Test
        [Fact]
        public void UpdateTreatment_WithParams_ReturnsUpdatedTreatment()
        {
            var serviceMock = new Mock<ITreatmentService>();
            var treatmentId = 1;
            serviceMock
                .Setup(s => s.UpdateTreatment(treatmentId))
                .Returns(new Treatments());
            Assert.NotNull(serviceMock.Object.UpdateTreatment(treatmentId));
        }
        #endregion
    }
    
}
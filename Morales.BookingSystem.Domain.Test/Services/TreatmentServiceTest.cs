using System;
using System.Collections.Generic;
using System.IO;
using Core.IServices;
using Core.Models;
using Moq;
using Morales.BookingSystem.Domain.IRepositories;
using Morales.BookingSystem.Domain.Services;
using NuGet.Frameworks;
using Xunit;

namespace Morales.BookingSystem.Domain.Test.Services
{
    public class TreatmentServiceTest
    {
        #region TreatmentService Initialization Tests

        [Fact]
        public void TreatmentService_IsITreatmentService()
        {
            var mockRepo = new Mock<ITreatmentRepository>();
            var treatmentService = new TreatmentService(mockRepo.Object);
            Assert.IsAssignableFrom<ITreatmentService>(treatmentService);
        }

        [Fact]
        public void TreatmentService_WithNullRepository_ThrowsInvalidDataException()
        {
            Assert
                .Throws<InvalidDataException>(() => new TreatmentService(null));
        }

        [Fact]
        public void TreatMentService_WithNullRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert
                .Throws<InvalidDataException>(() => new TreatmentService(null));
            Assert.Equal("TreatmentRepository Cannot Be Null!", exception.Message);
        }

        #endregion

        #region TreatmentService GetAllTreatments Tests

        [Fact]
        public void GetAll_NoParams_CallsTreatmentRepositoryOnce()
        {
            var mockRepo = new Mock<ITreatmentRepository>();
            var treatmentService = new TreatmentService(mockRepo.Object);

            treatmentService.GetAll();
            
            mockRepo.Verify(r => r.GetAll(), Times.Once);
        }
        [Fact]
        public void GetAll_NoParams_ReturnsAllTreatmentsAsList()
        {
            var expected = new List<Treatments>
                {new Treatments {Duration = DateTime.Now, Id = 1, Name = "Happy Ending"}};
            var mockRepo = new Mock<ITreatmentRepository>();
            mockRepo
                .Setup(s => s.GetAll())
                .Returns(expected);
            var treatmentService = new TreatmentService(mockRepo.Object);

            treatmentService.GetAll();
            
            Assert.Equal(expected, treatmentService.GetAll(), new TreatmentsComparer());

        }
        
        #endregion

        #region TreatmentService GetTreatment Tests

        [Fact]
        public void GetTreatment_WithParams_CallsTreatmentRepositoryOnce()
        {
            var mockRepo = new Mock<ITreatmentRepository>();
            var treatmentService = new TreatmentService(mockRepo.Object);
            var treatmentId = 1;

            treatmentService.GetTreatment(treatmentId);

            mockRepo.Verify(r => r.GetTreatment(treatmentId), Times.Once);
        }

        [Fact]
        public void GetTreatment_WithParams_ReturnsSingleTreatment()
        {
            var expected = new Treatments{Id = 1, Name = "Happy Ending"};
            var mockRepo = new Mock<ITreatmentRepository>();
            mockRepo
                .Setup(r => r.GetTreatment(expected.Id))
                .Returns(expected);
            var treatmentService = new TreatmentService(mockRepo.Object);

            treatmentService.GetTreatment(expected.Id);
            
            Assert.Equal(expected, treatmentService.GetTreatment(expected.Id), new TreatmentsComparer());
        }
        #endregion

        #region DeleteService Test

        [Fact]
        public void DeleteTreatment_WithParams_CallsTreatmentRepoOnce()
        {
            var mockRepo = new Mock<ITreatmentRepository>();
            var treatmentService = new TreatmentService(mockRepo.Object);
            var treatmentId = 1;

            treatmentService.DeleteTreatment(treatmentId);
            
            mockRepo.Verify(r => r.DeleteTreatment(treatmentId), Times.Once);
        }

        [Fact]
        public void DeleteTreatment_WithParams_ReturnsDeletedTreatment()
        {
            var expected = new Treatments() {Id = 1, Name = "Happy Ending"};
            var mockRepo = new Mock<ITreatmentRepository>();
            mockRepo
                .Setup(r => r.DeleteTreatment(expected.Id))
                .Returns(expected);
            var treatmentService = new TreatmentService(mockRepo.Object);

            treatmentService.DeleteTreatment(expected.Id);
            
            Assert.Equal(expected, treatmentService.DeleteTreatment(expected.Id), new TreatmentsComparer());
        }
            
        #endregion
    }

    #region Treatments Comparer
    public class TreatmentsComparer : IEqualityComparer<Treatments>
    {
        public bool Equals(Treatments x, Treatments y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Name == y.Name && x.Duration.Equals(y.Duration);
        }

        public int GetHashCode(Treatments obj)
        {
            return HashCode.Combine(obj.Id, obj.Name, obj.Duration);
        }
    }
    #endregion
}
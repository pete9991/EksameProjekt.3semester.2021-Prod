using System.Collections.Generic;
using Core.Models;
using Moq;
using Morales.BookingSystem.Domain.IRepositories;
using Xunit;
namespace Morales.BookingSystem.Domain.Test.IRepositories

{
    public class IAppointmentRepositoryTest
    {

        [Fact]
        public void IAppointmentRepository_Exists()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            Assert.NotNull(mockRepo.Object);
        }

        [Fact]
        public void ReadAllAppointments_ReturnsListOfAppointments()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.readAllAppointments())
                .Returns(new List<Appointment>());
            Assert.NotNull(mockRepo.Object.readAllAppointments());
        }

        [Fact]
        public void GetAppointment_WithParam_ReturnSingleAppointment()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var Id = (int)1;
            mockRepo
                .Setup(r => r.ReadById(Id))
                .Returns(new Appointment());
            Assert.NotNull(mockRepo.Object.ReadById(Id));
        }

        [Fact]
        public void CreateAppointment_WithParams_ReturnBoolean()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentToCreateId = new Appointment();
            mockRepo
                .Setup(r => r.CreateAppointment(appointmentToCreateId))
                .Returns(true);
            Assert.Equal(true, mockRepo.Object.CreateAppointment(appointmentToCreateId));
        }

        [Fact]
        public void UpdateAppointment_WithParams_ReturnAnAppointment()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentToUpdateId = 1;
            var updatedAppointment = new Appointment();
            mockRepo
                .Setup(r => r.UpdateById(appointmentToUpdateId, updatedAppointment))
                .Returns(new Appointment());
            Assert.NotNull(mockRepo.Object.UpdateById(appointmentToUpdateId, updatedAppointment));
        }

        [Fact]
        public void DeleteAppointment_WithParams_ReturnDeletedAppointment()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentToDeleteId = 1;
            mockRepo
                .Setup(r => r.DeleteAppointment(appointmentToDeleteId))
                .Returns(true);
            Assert.Equal(true, mockRepo.Object.DeleteAppointment(appointmentToDeleteId));
        }

        [Fact]
        public void GetAppointmentFromHairdresser_WithParams_ReturnAList()
        {
            var employeeId = 1;
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.GetAppointmentFromHairdresser(employeeId))
                .Returns(new List<Appointment>());
            Assert.NotNull(mockRepo.Object.GetAppointmentFromHairdresser(employeeId));
        }
        
    }
}
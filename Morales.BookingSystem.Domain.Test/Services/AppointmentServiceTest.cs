using System.IO;
using Core.IServices;
using Moq;
using Morales.BookingSystem.Domain.IRepositories;
using Morales.BookingSystem.Domain.Services;
using Xunit;

namespace Morales.BookingSystem.Domain.Test.Services
{
    public class AppointmentServiceTest
    {
        [Fact]
        public void AppointmentService_IsIAppointmentService()
        {
            var serviceMock = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(serviceMock.Object);
            Assert.IsAssignableFrom<IAppointmentService>(appointmentService);

        }

        [Fact]
        public void AppointmentService_WithNullRepository_ThrowInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new AppointmentService(null));
        }

        [Fact]
        public void AppointmentService_WithNullRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert
                .Throws<InvalidDataException>(() => new AppointmentService(null));
            Assert.Equal("A AppointmentService need an a  appointmentRepository", exception.Message);
        }
    }
}
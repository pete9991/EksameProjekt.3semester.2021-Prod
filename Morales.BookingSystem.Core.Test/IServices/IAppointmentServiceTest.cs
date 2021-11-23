using System.Collections.Generic;
using Core.IServices;
using Core.Models;
using Moq;
using Xunit;

namespace Morales.BookingSystem.Core.Test.IServices
{
    public class IAppointmentServiceTest
    {
        [Fact]
        public void IAppointmentService_Exists()
        {
            var serviceMock = new Mock<IAppointmentService>();
            Assert.NotNull(serviceMock.Object);
        }
        
        [Fact]
        public void ReadById_WithParams_ReturnSingleAppointment()
        {
            var serviceMock = new Mock<IAppointmentService>();
            var AppointmentId = 1;
            serviceMock.Setup(s => s.ReadById(AppointmentId))
                .Returns(new Appointment());
            Assert.NotNull(serviceMock.Object.ReadById(AppointmentId));
        }

        [Fact]
        public void CreateAppointment_WithParams_ReturnsBoolean()
        {
            var serviceMock = new Mock<IAppointmentService>();
            var AppointmentToCreate = new Appointment();
            var TestBoolean = true;
            serviceMock.Setup(s => s.CreateAppointment(AppointmentToCreate))
                .Returns(TestBoolean);
            Assert.NotNull(serviceMock.Object.CreateAppointment(AppointmentToCreate));
        }
        
        [Fact]
        public void UpdateById_WithParams_ReturnSingleAppointment()
        {
            var serviceMock = new Mock<IAppointmentService>();
            var AppointmentToUpdateId = 1;
            var UpdateAppointment = new Appointment();
            serviceMock.Setup(s => s.UpdateById(AppointmentToUpdateId, UpdateAppointment))
                .Returns(new Appointment());
            Assert.NotNull(serviceMock.Object.UpdateById(AppointmentToUpdateId, UpdateAppointment));
        }
        
        [Fact]
        public void DeleteAppointment_WithParams_ReturnsBoolean()
        {
            var serviceMock = new Mock<IAppointmentService>();
            var DeletedAppointmentId = 1;
            var TestBoolean = true;
            serviceMock.Setup(s => s.DeleteAppointment(DeletedAppointmentId))
                .Returns(TestBoolean);
            Assert.NotNull(serviceMock.Object.DeleteAppointment(DeletedAppointmentId));
        }

        [Fact]
        public void GetAllAppointments_WithNoParams_ReturnListOfAppointments()
        {
            var serviceMock = new Mock<IAppointmentService>();
            serviceMock.Setup(s => s.GetAllAppointments())
                .Returns(new List<Appointment>());
            Assert.NotNull(serviceMock.Object.GetAllAppointments());
        }

        [Fact]
        public void GetAppointmentsFromHairdresser_WithParams_ReturnListOfAppointments()
        {
            var serviceMock = new Mock<IAppointmentService>();
            var EmployeeId = 1;
            serviceMock.Setup(s => s.GetAppointmentsFromHairdresser(EmployeeId))
                .Returns(new List<Appointment>());
            Assert.NotNull(serviceMock.Object.GetAppointmentsFromHairdresser(EmployeeId));
        }

  
    }
}
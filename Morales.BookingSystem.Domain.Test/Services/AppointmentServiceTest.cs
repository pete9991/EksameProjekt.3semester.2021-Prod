using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Core.IServices;
using Core.Models;
using Moq;
using Morales.BookingSystem.Domain.IRepositories;
using Morales.BookingSystem.Domain.Services;
using Xunit;

namespace Morales.BookingSystem.Domain.Test.Services
{
    public class AppointmentServiceTest
    {
        #region Appointment Service Initalization Test
        
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
            Assert.Equal("An AppointmentService need an appointmentRepository", exception.Message);
        }
        #endregion

        #region Appointment Service GetAll Test


        [Fact]
        public void GetAllAppointments_WithNoParams_CallsAppointmentRepositoryOnce()
        {
            //Arrange
            var serviceMock = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(serviceMock.Object);
            
            //Act
            appointmentService.GetAllAppointments();
            
            //Assert
            serviceMock.Verify(r => r.readAllAppointments(),Times.Once);
        }

        [Fact]
        public void GetAllAppointment_NoParam_ReturnAllAppointmentsAsList()
        {
            //Arrange
            var expected = new List<Appointment> {new Appointment {Customerid = 1, Employeeid = 1}};
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.readAllAppointments())
                .Returns(expected);
            var appointmentService = new AppointmentService(mockRepo.Object);
            
            //Act
            appointmentService.GetAllAppointments();
            
            //Assert
            Assert.Equal(expected, appointmentService.GetAllAppointments(), new AppointmentComparer());
        }

        #endregion

        #region ReadById Test

        [Fact]
        public void ReadById_WithParams_CallAppointmentRepositoryOnce()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(mockRepo.Object);
            var appointmentId = 1;

            appointmentService.ReadById(appointmentId);
            
            mockRepo.Verify(r => r.ReadById(appointmentId),Times.Once);
        }

        [Fact]
        public void ReadById_WithParam_ReturnsSingleAppointment()
        {
            var expected = new Appointment {Id = 1, Customerid = 1, Employeeid = 1};
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.ReadById(expected.Id))
                .Returns(expected);
            var appointmentService = new AppointmentService(mockRepo.Object);
            var appointmentId = 1;

            appointmentService.ReadById(appointmentId);
            
            Assert.Equal(expected, appointmentService.ReadById(expected.Id), new AppointmentComparer());

        }

        #endregion

        #region Create Appointment Test
        
        [Fact]
        public void CreateAppointment_WithParam_CallsAppointmentRepositoryOnce()
        { 
            var TestList = new List<Appointment>(); 
            var testAppointment = new Appointment {Id = 1, Customerid = 1, Employeeid = 1, TreatmentsList = new List<Treatments>()};
          var mockRepo = new Mock<IAppointmentRepository>();
          mockRepo
              .Setup(r => r.readAllAppointments())
              .Returns(TestList);
          var appointmentService = new AppointmentService(mockRepo.Object);

          appointmentService.CreateAppointment(testAppointment);
            
          mockRepo.Verify(r => r.CreateAppointment(testAppointment), Times.Once);
        }

        [Fact]
        public void CreateAppointment_WithParam_ReturnsAppointmentWhenCompleted()
        {
            var TestList = new List<Appointment>();
            var testAppointment = new Appointment {Id = 1, Customerid = 1, Employeeid = 1, TreatmentsList = new List<Treatments>()};
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.CreateAppointment(testAppointment))
                .Returns(new Appointment());
            mockRepo
                .Setup(r => r.readAllAppointments())
                .Returns(TestList);
            var appointmentService = new AppointmentService(mockRepo.Object);

            appointmentService.CreateAppointment(testAppointment);
            
            Assert.NotNull(appointmentService.CreateAppointment(testAppointment));
        }

        #endregion

        #region Update Appointment Test

        
        [Fact]
        public void UpdateAppointment_WithParam_CallsAppointmentRepositoryOnce()
        {
            var testList = new List<Appointment>();
          var appointmentToUpdateId = 1;
          var updatedAppointment = new Appointment {Id = 1, Employeeid = 1, Customerid = 1, TreatmentsList = new List<Treatments>()};
          var mockRepo = new Mock<IAppointmentRepository>();
          mockRepo
              .Setup(r => r.readAllAppointments())
              .Returns(testList);
          var appointmentService = new AppointmentService(mockRepo.Object);

          appointmentService.UpdateById(appointmentToUpdateId,updatedAppointment);
            
          mockRepo.Verify(r => r.UpdateById(appointmentToUpdateId, updatedAppointment), Times.Once);
        }

        [Fact]
        public void UpdateAppointment_WithParam_ReturnsAppointment()
        {
            var TestList = new List<Appointment>();
            var appointmentToUpdateId = 1;
            var updatedAppointment = new Appointment {Id = 1, Employeeid = 1, Customerid = 1, TreatmentsList = new List<Treatments>()};
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.UpdateById(appointmentToUpdateId, updatedAppointment))
                .Returns(updatedAppointment);
            mockRepo
                .Setup(r => r.readAllAppointments())
                .Returns(TestList);
            var appointmentService = new AppointmentService(mockRepo.Object);

            appointmentService.UpdateById(appointmentToUpdateId, updatedAppointment);
            
            Assert.Equal(updatedAppointment, appointmentService.UpdateById(appointmentToUpdateId,updatedAppointment), new AppointmentComparer());
        }

        #endregion

        #region Delete Appointment Test

        [Fact]
        public void DeleteAppointment_WithParams_CallsAppointmentRepositoryOnce()
        {
            var testId = 1;
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(mockRepo.Object);

            appointmentService.DeleteAppointment(testId);
            
            mockRepo.Verify(r => r.DeleteAppointment(testId), Times.Once);
        }

        [Fact]
        public void DeleteAppointment_WithParam_ReturnAppointment()
        {
            var testId = 1;
            var expected = new Appointment();
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.DeleteAppointment(testId))
                .Returns(expected);
            var appointmentService = new AppointmentService(mockRepo.Object);

            appointmentService.DeleteAppointment(testId);
            
            Assert.NotNull(appointmentService.DeleteAppointment(testId));
        }
        
        #endregion

        #region Get Appointment From Hairdresser Test
        
        [Fact]
        public void GetAppointmentsFromHairdresser_WithNoParams_CallAppointmentRepositoryOnce()
        {
            var employeeId = 1;
            var serviceMock = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(serviceMock.Object);

            appointmentService.GetAppointmentsFromHairdresser(employeeId);
            
            serviceMock.Verify(r => r.GetAppointmentFromHairdresser(employeeId),Times.Once);
        }

        [Fact]
        public void GetAppointmentsFromHairdresser_WithNoParam_ReturnAllAppointmentsAsList()
        {
            var employeeId = 1;
            var expected = new List<Appointment> {new Appointment {Employeeid = 1}};
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.GetAppointmentFromHairdresser(employeeId))
                .Returns(expected);
            var appointmentService = new AppointmentService(mockRepo.Object);

            appointmentService.GetAppointmentsFromHairdresser(employeeId:1);
            
            Assert.Equal(expected, appointmentService.GetAppointmentsFromHairdresser(employeeId:1), new AppointmentComparer());

        }
        
        #endregion

        #region price Calculation test

        [Fact]
        public void CalculatePrice_withParams_EmptyListOfTreaments()
        {
            var serviceMock = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(serviceMock.Object);
            var treamentList = new List<Treatments>();
            double expectedResult = 0;
            var testApointment = new Appointment {TreatmentsList = treamentList};
            Assert.Equal(expectedResult, appointmentService.CalculateTotalPrice(testApointment));
        }
        
        [Fact]
        public void CalculatePrice_withParams_onlyOneTreament()
        {
            var serviceMock = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(serviceMock.Object);
            var treamentList = new List<Treatments>();
            treamentList.Add(new Treatments {Id = 1, Duration = new TimeSpan(0, 30, 0), Price = 150});
            double expectedResult = 150;
            var testApointment = new Appointment {TreatmentsList = treamentList};
            Assert.Equal(expectedResult, appointmentService.CalculateTotalPrice(testApointment));
        }

        [Fact]
        public void CalculatePrice_withParams_ListWithMultipleTreatments()
        {
            var serviceMock = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(serviceMock.Object);
            var treamentList = new List<Treatments>();
            treamentList.Add(new Treatments {Id = 1, Duration = new TimeSpan(0, 30, 0), Price = 150});
            treamentList.Add(new Treatments {Id = 2, Duration = new TimeSpan(0, 30, 0), Price = 150});
            treamentList.Add(new Treatments {Id = 3, Duration = new TimeSpan(0, 30, 0), Price = 100});
            double expectedResult = 400;
            var testApointment = new Appointment {TreatmentsList = treamentList};
            Assert.Equal(expectedResult, appointmentService.CalculateTotalPrice(testApointment));
        }

        #endregion

        #region Duation and appointment calculation Test

        [Fact]
        public void CalculateDuration_WithParam_ReturnTimespan()
        {
            var timespan = new TimeSpan(0, 0, 0);
            var appointment = new Appointment
            {
                Duration = timespan,
                TreatmentsList = new List<Treatments>()
            };
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(mockRepo.Object);
            
            appointmentService.CalculateDuration(appointment);
            
            Assert.Equal(timespan,appointmentService.CalculateDuration(appointment));
        }

        [Fact]
        public void CalculateDuration_WithParam_ReturnDurationOfAppointmentWithOneTreatment()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(mockRepo.Object);
            var treamentList = new List<Treatments>();
            treamentList.Add(new Treatments {Id = 1, Duration = new TimeSpan(0, 30, 0), Price = 150});
            var testAppointment = new Appointment
            {
                TreatmentsList = treamentList
            };
            var expectedResult = new TimeSpan(0, 30, 0);

            appointmentService.CalculateDuration(testAppointment);
            
            Assert.Equal(expectedResult, appointmentService.CalculateDuration(testAppointment));

        }

        [Fact]
        public void CalculateDuration_WithParam_ReturnTotalDurationOfAllTreatmentsInList()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(mockRepo.Object);
            var treamentList = new List<Treatments>();
            treamentList.Add(new Treatments {Id = 1, Duration = new TimeSpan(0, 30, 0), Price = 150});
            treamentList.Add(new Treatments {Id = 2, Duration = new TimeSpan(0, 30, 0), Price = 150});
            treamentList.Add(new Treatments {Id = 3, Duration = new TimeSpan(0, 30, 0), Price = 150});
            var testAppointment = new Appointment
            {
                TreatmentsList = treamentList
            };
            var expectedResult = new TimeSpan(1, 30, 0);

            appointmentService.CalculateDuration(testAppointment);
            
            Assert.Equal(expectedResult, appointmentService.CalculateDuration(testAppointment));
        }

        [Fact]
        public void CalculateAppointmentEnd_WithParam_ReturnDateTime()
        {
            var datetime = new DateTime();
            datetime = DateTime.Today;
            var appointment = new Appointment
            {
                AppointmentEnd = datetime
            };
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(mockRepo.Object);
            
            appointmentService.CalculateAppointmentEnd(appointment);
        }

        [Fact]
        public void CalculateAppointmentEnd_WithParam_CalculateCorrectEndTime()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(mockRepo.Object);
            var startTime = new DateTime();
            startTime = DateTime.Now;

            var expectedResult = startTime.AddHours(1);
            var duration = new TimeSpan(1, 0, 0);

            var testAppointment = new Appointment
            {
                Date = startTime,
                Duration = duration
            };
            appointmentService.CalculateAppointmentEnd(testAppointment);
            
            Assert.Equal(expectedResult, appointmentService.CalculateAppointmentEnd(testAppointment));

        }
        
        #endregion

        #region Appointment conflict tests

        [Fact]
        public void ConflictDetector_withParams_doseNothingWhenNoConflictIsPresent()
        {
            var TestDbAppointment = new Appointment
            {
                Date = new DateTime(2020, 6, 15, 12, 30, 0),
                AppointmentEnd = new DateTime(2020, 6, 15, 13, 0, 0)
            };
            var TestAppointment = new Appointment
            {
                Date = new DateTime(2020, 6, 16, 12, 30, 0),
                AppointmentEnd = new DateTime(2020, 6, 16, 13, 0, 0)
            };
            var TestList = new List<Appointment>();
            TestList.Add(TestDbAppointment);
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.readAllAppointments())
                .Returns(TestList);
            var appointmentService = new AppointmentService(mockRepo.Object);
            appointmentService.DetectAppointmentConflict(TestAppointment);
        }

        [Fact]
        public void ConflictDetection_withParams_throwsExceptionWhenStartIsDuringAnotherAppointment()
        {
            var TestDbAppointment = new Appointment
            {
                Date = new DateTime(2020, 6, 15, 12, 30, 0),
                AppointmentEnd = new DateTime(2020, 6, 15, 13, 0, 0)
            };
            var TestAppointment = new Appointment
            {
                Date = new DateTime(2020, 6, 15, 12, 45, 0),
                AppointmentEnd = new DateTime(2020, 6, 15, 13, 15, 0)
            };
            var TestList = new List<Appointment>();
            TestList.Add(TestDbAppointment);
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.readAllAppointments())
                .Returns(TestList);
            var appointmentService = new AppointmentService(mockRepo.Object);
            
            var ex = Assert.Throws<InvalidDataException>(() => appointmentService.DetectAppointmentConflict(TestAppointment));
            Assert.Equal("Invalid time, this appointment would start during another appointment", ex.Message);
        }
        [Fact]
        public void ConflictDetection_withParams_throwsExceptionWhenEndIsDuringAnotherAppointment()
        {
            var TestDbAppointment = new Appointment
            {
                Date = new DateTime(2020, 6, 15, 12, 30, 0),
                AppointmentEnd = new DateTime(2020, 6, 15, 13, 0, 0)
            };
            var TestAppointment = new Appointment
            {
                Date = new DateTime(2020, 6, 15, 12, 0, 0),
                AppointmentEnd = new DateTime(2020, 6, 15, 12, 45, 0)
            };
            var TestList = new List<Appointment>();
            TestList.Add(TestDbAppointment);
            var mockRepo = new Mock<IAppointmentRepository>();
            mockRepo
                .Setup(r => r.readAllAppointments())
                .Returns(TestList);
            var appointmentService = new AppointmentService(mockRepo.Object);
            
            var ex = Assert.Throws<InvalidDataException>(() => appointmentService.DetectAppointmentConflict(TestAppointment));
            Assert.Equal("Invalid time, this appointment would end during another appointment", ex.Message);
        }
        

        #endregion
    }
    
    public class AppointmentComparer : IEqualityComparer<Appointment>
    {
        public bool Equals(Appointment x, Appointment y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Customerid == y.Customerid && x.Employeeid == y.Employeeid && x.Date.Equals(y.Date) && x.Duration == y.Duration;
        }

        public int GetHashCode(Appointment obj)
        {
            return HashCode.Combine(obj.Id, obj.Customerid, obj.Employeeid, obj.Date, obj.Duration);
        }
    }
}
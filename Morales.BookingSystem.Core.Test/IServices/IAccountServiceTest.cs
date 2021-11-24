using System.Collections.Generic;
using System.Net.Http.Headers;
using Core.IServices;
using Core.Models;
using Moq;
using NuGet.Frameworks;
using Xunit;

namespace Morales.BookingSystem.Core.Test.IServices
{
    public class IAccountServiceTest
    {
        [Fact]
        public void IAccountService_Exists()
        {
            var serviceMock = new Mock<IAccountService>();
            Assert.NotNull(serviceMock);
        }

        [Fact]
        public void GetAll_WithNoParams_ReturnListOfAccounts()
        {
            var serviceMock = new Mock<IAccountService>();
            serviceMock
                .Setup(s => s.GetAll())
                .Returns(new List<Account>());
        }

        [Fact]
        public void GetAccount_WithParams_ReturnsSingleAccount()
        {
            var serviceMock = new Mock<IAccountService>();
            var accountId = 1;
            serviceMock
                .Setup(s => s.GetAccount(accountId))
                .Returns(new Account());
            Assert.NotNull(serviceMock.Object);
        }

        [Fact]
        public void DeleteAccount_WithParams_ReturnsDeletedAccount()
        {
            var serviceMock = new Mock<IAccountService>();
            var accountId = 1;
            serviceMock
                .Setup(s => s.DeleteAccount(accountId))
                .Returns(new Account());
            Assert.NotNull(serviceMock.Object);
        }

        [Fact]
        public void CreateAccount_ReturnsCreatedAccount()
        {
            var serviceMock = new Mock<IAccountService>();
            var account = new Account
            {
                Id = 1,
                Type = "Costumer",
                Name = "Bobbe",
                PhoneNumber = "12345678",
                Sex = "yes",
                Email = "popbob@bob.com"
            };
            serviceMock
                .Setup(s => s.CreateAccount(account))
                .Returns(new Account{ Type = account.Type, Name = account.Name, PhoneNumber = account.PhoneNumber, Sex = account.Sex, Email = account.Email});
            Assert.NotNull(serviceMock.Object);
        }

        [Fact]
        public void UpdateAccount_ReturnsAccountWithUpdatedInformation()
        {
            var serviceMock = new Mock<IAccountService>();
            var accountToUpdate = new Account
            {
                Id = 1,
                Type = "Ost",
                Name = "Port Salut",
                PhoneNumber = "12345678",
                Sex = "yes",
                Email = "Ost@Ost.dk"
            };
            serviceMock
                .Setup(s => s.UpdateAccount(accountToUpdate))
                .Returns(new Account
                {
                    Id = accountToUpdate.Id,
                    Type = accountToUpdate.Type,
                    Name = accountToUpdate.Name,
                    PhoneNumber = accountToUpdate.PhoneNumber,
                    Sex = accountToUpdate.Sex,
                    Email = accountToUpdate.Email
                });
            Assert.NotNull(serviceMock.Object);
        }

    }
}
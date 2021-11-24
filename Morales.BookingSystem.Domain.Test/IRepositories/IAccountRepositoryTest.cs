using System.Collections.Generic;
using Core.Models;
using Moq;
using Morales.BookingSystem.Domain.IRepositories;
using Morales.BookingSystem.Domain.Services;
using Xunit;

namespace Morales.BookingSystem.Domain.Test.IRepositories
{
    public class IAccountRepositoryTest
    {
        [Fact]
        public void IAccountRepository_Exists()
        {
            var repoMock = new Mock<IAccountRepository>();
            Assert.NotNull(repoMock.Object);
        }

        [Fact]
        public void GetAll_WithNoParams_ReturnsListOfAccounts()
        {
            var repoMock = new Mock<IAccountRepository>();
            repoMock
                .Setup(s => s.GetAll())
                .Returns(new List<Account>());
            Assert.NotNull(repoMock.Object.GetAll());
        }

        [Fact]
        public void GetAccount_WithParams_ReturnsSingleAccount()
        {
            var repoMock = new Mock<IAccountRepository>();
            var accountId = 1;
            repoMock
                .Setup(s => s.GetAccount(accountId))
                .Returns(new Account());
            Assert.NotNull(repoMock.Object.GetAccount(accountId));
        }

        [Fact]
        public void DeleteAccount_WithParams_ReturnsDeletedAccount()
        {
            var repoMock = new Mock<IAccountRepository>();
            var accountId = 1;
            repoMock
                .Setup(s => s.DeleteAccount(accountId))
                .Returns(new Account());
            Assert.NotNull(repoMock.Object.DeleteAccount(accountId));
        }

        [Fact]
        public void CreateAccount_ReturnsCreatedAccount()
        {
            var account = new Account
            {
                Id = 1,
                Type = "Costumer",
                Name = "Bobbe",
                PhoneNumber = "12345678",
                Sex = "yes",
                Email = "popbob@bob.com"
            };
            var repoMock = new Mock<IAccountRepository>();
            repoMock
                .Setup(s => s.CreateAccount(account))
                .Returns(new Account());
            Assert.NotNull(repoMock.Object.CreateAccount(account));
        }
    }
}
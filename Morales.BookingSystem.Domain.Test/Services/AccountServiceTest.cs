using System;
using System.Collections.Generic;
using System.IO;
using Core.IServices;
using Core.Models;
using Moq;
using Morales.BookingSystem.Domain.IRepositories;
using Morales.BookingSystem.Domain.Services;
using Xunit;

namespace Morales.BookingSystem.Domain.Test.Services
{
    public class AccountServiceTest
    {
        #region AccountService Initialization Tests
        
        [Fact]
        public void AccountService_IsIAccountService()
        {
            var mockRepo = new Mock<IAccountRepository>();
            var accountService = new AccountService(mockRepo.Object);
            Assert.IsAssignableFrom<IAccountService>(accountService);
        }

        [Fact]
        public void AccountService_WithNullRepository_ThrowInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new AccountService(null));
        }

        [Fact]
        public void AccountService_WithNullRepository_ThrowsExceptionWithMessage()
        {
            var e = Assert.Throws<InvalidDataException>(() => new AccountService(null));
            Assert.Equal("AccountRepository cannot be Null!", e.Message);
        }
        #endregion

        #region AccountService GetAllAccounts Tests

        [Fact]
        public void GetAll_NoParams_CallsAccountRepositoryOnce()
        {
            var mockRepo = new Mock<IAccountRepository>();
            var accountService = new AccountService(mockRepo.Object);

            accountService.GetAll();
            
            mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAll_NoParams_ReturnsAllAccountsAsList()
        {
            var expected = new List<Account> {new Account {Id = 1, Name = "Ostekage"}};
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo
                .Setup(s => s.GetAll())
                .Returns(expected);
            var accountService = new AccountService(mockRepo.Object);

            accountService.GetAll();
            
            Assert.Equal(expected, accountService.GetAll(), new AccountComparer());
        }

        #endregion

        #region AccountService GetProduct Tests

        [Fact]
        public void GetAccount_WithParams_CallsAccountRepositoryOnce()
        {
            var mockRepo = new Mock<IAccountRepository>();
            var accountService = new AccountService(mockRepo.Object);
            var accountId = 1;

            accountService.GetAccount(accountId);
            
            mockRepo.Verify(r => r.GetAccount(accountId), Times.Once);
        }

        [Fact]
        public void GetAccount_WithParams_ReturnsSingleProduct()
        {
            var expected = new Account {Id = 1, Name = "Brie"};
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo
                .Setup(s => s.GetAccount(expected.Id))
                .Returns(expected);
            var accountService = new AccountService(mockRepo.Object);

            accountService.GetAccount(expected.Id);
            
            Assert.Equal(expected, accountService.GetAccount(expected.Id), new AccountComparer());
        }
        
        

        #endregion

        #region AccountService DeleteAccount Test

        [Fact]
        public void DeleteAccount_WithParams_CallsAccountRepositoryOnce()
        {
            var mockRepo = new Mock<IAccountRepository>();
            var accountService = new AccountService(mockRepo.Object);
            var accountId = 1;

            accountService.DeleteAccount(accountId);
            
            mockRepo.Verify(s => s.DeleteAccount(accountId), Times.Once);
        }

        [Fact]
        public void DeleteAccount_WithParams_ReturnsSingleProduct()
        {
            var expected = new Account {Id = 1, Name = "Brie"};
            var mockRepo = new Mock<IAccountRepository>();
                mockRepo
                .Setup(s => s.DeleteAccount(expected.Id))
                .Returns(expected);
                
            var accountService = new AccountService(mockRepo.Object);
            
            Assert.Equal(expected,accountService.DeleteAccount(expected.Id), new AccountComparer());
        }

        [Fact]
        public void UpdateAccount_WithParams_ReturnsSingleAccount()
        {
            var expected = new Account
            {
                Id = 1,
                Type = "Ost",
                Name = "Port Salut",
                PhoneNumber = "12345678",
                Sex = "yes",
                Email = "Ost@Ost.dk"
            };
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo
                .Setup(s => s.UpdateAccount(expected))
                .Returns(expected);
            var accountService = new AccountService(mockRepo.Object);
            Assert.Equal(expected, accountService.UpdateAccount(expected), new AccountComparer());
        }

        #endregion

    }

    #region Account Comparer
    public class AccountComparer : IEqualityComparer<Account>
    {
        public bool Equals(Account x, Account y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Type == y.Type && x.Name == y.Name && x.PhoneNumber == y.PhoneNumber && x.Sex == y.Sex && x.Email == y.Email;
        }

        public int GetHashCode(Account obj)
        {
            return HashCode.Combine(obj.Id, obj.Type, obj.Name, obj.PhoneNumber, obj.Sex, obj.Email);
        }
    }
    #endregion
}
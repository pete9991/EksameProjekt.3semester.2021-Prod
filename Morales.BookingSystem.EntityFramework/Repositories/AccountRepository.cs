using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Morales.BookingSystem.Domain.IRepositories;
using Morales.BookingSystem.EntityFramework.Entities;

namespace Morales.BookingSystem.EntityFramework.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MainDbContext _ctx;

        public AccountRepository(MainDbContext ctx)
        {
            _ctx = ctx;
        }
        public List<Account> GetAll()
        {
            return _ctx.Accounts.Select(ae => new Account
                {
                    Id = ae.Id,
                    Type = ae.Type,
                    Name = ae.Name,
                    PhoneNumber = ae.PhoneNumber,
                    Sex = ae.Sex,
                    Email = ae.Email
                })
                .ToList();
        }

        public Account GetAccount(int accountId)
        {
            return _ctx.Accounts.Select(ae => new Account
                {
                    Id = ae.Id,
                    Type = ae.Type,
                    Name = ae.Name,
                    PhoneNumber = ae.PhoneNumber,
                    Sex = ae.Sex,
                    Email = ae.Email
                })
                .FirstOrDefault(a => a.Id == accountId);
        }
        public Account GetAccountFromPhoneNumber(string phoneNumber)
        {
            return _ctx.Accounts.Select(ae => new Account
                {
                    Id = ae.Id,
                    Type = ae.Type,
                    Name = ae.Name,
                    PhoneNumber = ae.PhoneNumber,
                    Sex = ae.Sex,
                    Email = ae.Email
                })
                .FirstOrDefault(a => a.PhoneNumber == phoneNumber);
        }

        public Account DeleteAccount(int accountId)
        {
            var accountToDelete = _ctx.Accounts
                .Select(ae => new Account
                {
                    Id = ae.Id,
                    Type = ae.Type,
                    Name = ae.Name,
                    PhoneNumber = ae.PhoneNumber,
                    Sex = ae.Sex,
                    Email = ae.Email
                })
                .FirstOrDefault(a => a.Id == accountId);
            _ctx.Accounts.Remove(new AccountEntity() {Id = accountId});
            _ctx.SaveChanges();
            return accountToDelete;
        }

        public Account CreateAccount(Account accountToCreate)
        {
            
            var entity = _ctx.Add(new AccountEntity()
            {
                Type = accountToCreate.Type,
                Name = accountToCreate.Name,
                PhoneNumber = accountToCreate.PhoneNumber,
                Sex = accountToCreate.Sex,
                Email = accountToCreate.Email
            }).Entity;
            _ctx.SaveChanges();
            return new Account()
            {
                Type = entity.Type,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Sex = entity.Sex,
                Email = entity.Email
            };
        }

        public Account UpdateAccount(Account accountToUpdate)
        {
            var accountEntity = new AccountEntity()
            {
                Id = accountToUpdate.Id,
                Type = accountToUpdate.Type,
                Name = accountToUpdate.Name,
                PhoneNumber = accountToUpdate.PhoneNumber,
                Sex = accountToUpdate.Sex,
                Email = accountToUpdate.Email
            };
            var entity = _ctx.Update(accountEntity).Entity;
            _ctx.SaveChanges();
            return new Account
            {
                Id = entity.Id,
                Type = entity.Type,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Sex = entity.Sex,
                Email = entity.Email
            };
        }
    }
}
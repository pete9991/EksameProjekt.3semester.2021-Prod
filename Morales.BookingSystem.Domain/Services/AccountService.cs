using System.Collections.Generic;
using System.IO;
using Core.IServices;
using Core.Models;
using Morales.BookingSystem.Domain.IRepositories;

namespace Morales.BookingSystem.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;

        public AccountService(IAccountRepository accountRepository)
        {
            if (accountRepository == null)
            {
                throw new InvalidDataException("AccountRepository cannot be Null!");
            }

            _accountRepo = accountRepository;
        }

        public List<Account> GetAll()
        {
            return _accountRepo.GetAll();
        }

        public Account GetAccount(int accountId)
        {
            return _accountRepo.GetAccount(accountId);
        }

        public Account DeleteAccount(int accountId)
        {
            return _accountRepo.DeleteAccount(accountId);
        }

        public Account CreateAccount( string type, string name, string phoneNumber, string sex, string email)
        {
            return _accountRepo.CreateAccount(type, name, phoneNumber, sex, email);
        }

        public Account UpdateAccount(Account accountToUpdate)
        {
            return _accountRepo.UpdateAccount(accountToUpdate);
        }
    }
}
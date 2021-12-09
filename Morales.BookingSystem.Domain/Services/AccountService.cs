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

        public Account CreateAccount(Account accountToCreate)
        {
            return _accountRepo.CreateAccount(accountToCreate);
        }

        public Account UpdateAccount(Account accountToUpdate)
        {
            return _accountRepo.UpdateAccount(accountToUpdate);
        }

        public Account GetAccountFromPhoneNumber(string phoneNumber)
        {
           return _accountRepo.GetAccountFromPhoneNumber(phoneNumber);
        }

        public List<Account> getAccountFromType(string type)
        {
            return _accountRepo.GetAccountFromType(type);
        }
    }
}
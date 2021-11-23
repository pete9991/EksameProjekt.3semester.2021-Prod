using System.Collections.Generic;
using Core.Models;

namespace Morales.BookingSystem.Domain.IRepositories
{
    public interface IAccountRepository
    {
        public List<Account> GetAll();
        public Account GetAccount(int accountId);
        public Account DeleteAccount(int accountId);
        public Account CreateAccount(string type, string name, string phonenumber, string sex, string email);
        public Account UpdateAccount(Account account);
    }
}
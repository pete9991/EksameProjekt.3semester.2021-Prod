using System.Collections.Generic;
using Core.Models;

namespace Core.IServices
{
    public interface IAccountService
    {
        public List<Account> GetAll();
        public Account GetAccount(int accountId);
        public Account DeleteAccount(int accountId);
        public Account CreateAccount( string type, string name, string phoneNumber, string sex, string email);
        public Account UpdateAccount(Account accountToUpdate);
    }
}
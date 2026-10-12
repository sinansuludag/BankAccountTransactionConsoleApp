using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAccountService
    {
        List<Account> GetAllAccounts();
        Account GetAccountById(int id);
        List<Account> GetAccountsByCustomerId(int customerId);
        bool AddAccount(Account account);
        bool DeleteAccount(int id);
        bool Deposit(int accountId, decimal amount);
        bool Withdraw(int accountId, decimal amount);
    }
}

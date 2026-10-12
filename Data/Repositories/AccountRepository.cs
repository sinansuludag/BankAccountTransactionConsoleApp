using Data.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public List<Account> GetAccountsByCustomerId(int customerId)
        {
            var list = GetAll().Where(x=>x.CustomerId==customerId).ToList();
            return list;
        }

        public Account? GetByAccountNumber(int accountNumber)
        {
            return GetAll().FirstOrDefault(x=>x.AccountNumber==accountNumber);
        }
    }
}

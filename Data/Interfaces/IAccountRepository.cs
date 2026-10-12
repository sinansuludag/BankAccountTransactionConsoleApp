using Data.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IAccountRepository: IRepositoryBase<Account>
    {
        List<Account> GetAccountsByCustomerId(int customerId);
        Account? GetByAccountNumber(int accountNumber);
    }
}

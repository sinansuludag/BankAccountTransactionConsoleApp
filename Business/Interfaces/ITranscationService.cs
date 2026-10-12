using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITranscationService
    {
        List<Transaction> GetTransactionsByAccountId(int accountId);
        Transaction? GetTransactionById(int id);
        List<Transaction> GetAllTransactions();
    }
}

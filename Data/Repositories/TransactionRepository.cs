using Data.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITranscationRepository
    {
        public List<Transaction> GetTransactionsByAccountId(int accountId)
        {
            return GetAll()
                .Where(x => x.AccountId == accountId)
                .ToList();
        }

        public int GetNextId()
        {
            var transactions = GetAll();

            if (transactions.Count == 0)
                return 1;

            return transactions.Max(x => x.Id) + 1;
        }
    }
}

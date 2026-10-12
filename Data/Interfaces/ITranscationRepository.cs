using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ITranscationRepository : IRepositoryBase<Transaction>
    {
        List<Transaction> GetTransactionsByAccountId(int accountId);
        int GetNextId();
    }
}

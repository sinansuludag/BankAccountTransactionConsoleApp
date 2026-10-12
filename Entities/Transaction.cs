using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Transaction: IEntity
    {
        public int Id { get; set; }

    
        public int AccountId { get; set; }

        
        public decimal Amount { get; set; }

     
        public TransactionType TransactionType { get; set; }

      
        public DateTime TransactionDate { get; set; }

    
        public int? SourceAccountId { get; set; }

        public int? DestinationAccountId { get; set; }
    }

    public enum TransactionType
    {
        Deposit,
        Withdraw,
        Transfer
    }
}

using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Transfer: IEntity
    {
        public int Id { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public TransferType TransferType { get; set; }
        public DateTime TransferTarihi { get; set; }
        public String? Description { get; set; }

    }

    public enum TransferType
    {
        Havale,
        EFT,
        Fast
    }
}

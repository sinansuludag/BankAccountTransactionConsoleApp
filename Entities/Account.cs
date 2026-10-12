using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        public  int AccountNumber { get; set; }
        public  decimal Balance { get; set; }
        public  int CustomerId { get; set; }
       
    }
}

using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Customer? GetByTc(string tc);
    }
}

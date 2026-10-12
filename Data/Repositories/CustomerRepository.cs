using Data.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public Customer? GetByTc(string tc)
        {
            return GetAll().FirstOrDefault(x => x.Tc == tc);
        }
    }
}

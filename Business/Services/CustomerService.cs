using Business.Interfaces;
using Data.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository) {
           _customerRepository = customerRepository;
        }
        public bool AddCustomer(Customer customer)
        {
            if (customer == null)
                return false;

            if (customer.Id <= 0 ||
                string.IsNullOrWhiteSpace(customer.FirstName) ||
                string.IsNullOrWhiteSpace(customer.LastName) ||
                string.IsNullOrWhiteSpace(customer.Tc) ||
                string.IsNullOrWhiteSpace(customer.Phone))
            {
                return false;
            }

            customer.Tc = customer.Tc.Trim();

            var entityId = _customerRepository.GetById(customer.Id);

            if (entityId != null)
                return false;

            var entityTc = _customerRepository.GetByTc(customer.Tc);

            if (entityTc != null)
                return false;

            return _customerRepository.Add(customer);
        }

        public bool DeleteCustomer(int id)
        {
            var customer = _customerRepository.GetById(id);

            if (customer == null)
                return false;

            return _customerRepository.Delete(id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer? GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public bool UpdateCustomer(Customer customer)
        {
            if (customer == null)
                return false;

            if (customer.Id <= 0 ||
                string.IsNullOrWhiteSpace(customer.FirstName) ||
                string.IsNullOrWhiteSpace(customer.LastName) ||
                string.IsNullOrWhiteSpace(customer.Tc) ||
                string.IsNullOrWhiteSpace(customer.Phone))
            {
                return false;
            }

            customer.Tc = customer.Tc.Trim();

            var existingCustomer =
                _customerRepository.GetById(customer.Id);

            if (existingCustomer == null)
                return false;

            var customerWithSameTc =
                _customerRepository.GetByTc(customer.Tc);

            if (customerWithSameTc != null &&
                customerWithSameTc.Id != customer.Id)
            {
                return false;
            }

            existingCustomer.WithCustomer(
                firstName: customer.FirstName,
                lastName: customer.LastName,
                tc: customer.Tc,
                phone: customer.Phone
            );

            return _customerRepository.Update(existingCustomer);
        }
    }
}

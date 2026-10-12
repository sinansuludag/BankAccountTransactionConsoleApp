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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITranscationRepository _transcationRepository;

        public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository, ITranscationRepository transcationRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _transcationRepository = transcationRepository;
        }
                
        public bool AddAccount(Account account)
        {
            if(account==null) return false;
            if (account.Id <= 0)
                return false;
            if (account.CustomerId <= 0)
                return false;

            var exsistingCustomer=_customerRepository.GetById(account.CustomerId);
            if(exsistingCustomer==null) return false;

            var exsistingId= _accountRepository.GetById(account.Id);

            if( exsistingId!=null) return false;

            var exsisingtAccountNumber = _accountRepository.GetByAccountNumber(account.AccountNumber);

            if(exsisingtAccountNumber!=null) return false;

            if(account.Balance <0) return false;

            if(account is VadeliAccount vadeliAccount)
            {
                if(vadeliAccount.FaizOrani<=0) return false;
                if(vadeliAccount.VadeBitisTarihi<=vadeliAccount.VadeBaslangicTarihi) return false;
            }


            return _accountRepository.Add(account);

        }

        public bool DeleteAccount(int id)
        {
            if(id<=0) return false;
            var existingAccount= _accountRepository.GetById(id);
            if(existingAccount==null) return false;
            if(existingAccount.Balance>0) return false;

            return _accountRepository.Delete(id);
        }

        public bool Deposit(int accountId, decimal amount)
        {
            if (accountId <= 0)
                return false;

            if (amount <= 0)
                return false;

            var existingAccount = _accountRepository.GetById(accountId);

            if (existingAccount == null)
                return false;

            existingAccount.Balance += amount;

            Transaction transaction = new Transaction
            {   Id = _transcationRepository.GetNextId(),
                AccountId = accountId,
                Amount = amount,
                TransactionType = TransactionType.Deposit,
                TransactionDate = DateTime.Now
            };

            bool resultDeposit = _accountRepository.Update(existingAccount);

            if (!resultDeposit)
                return false;

            bool resultTransaction = _transcationRepository.Add(transaction);

            if (!resultTransaction)
            {
                existingAccount.Balance -= amount;
                _accountRepository.Update(existingAccount);
                return false;
            }

            return true;
        }


        public bool Withdraw(int accountId, decimal amount)
        {
            if (accountId <= 0)
                return false;

            if (amount <= 0)
                return false;

            var existingAccount = _accountRepository.GetById(accountId);

            if (existingAccount == null)
                return false;

            if (amount > existingAccount.Balance)
                return false;

            existingAccount.Balance -= amount;

            Transaction transaction = new Transaction
            {   Id = _transcationRepository.GetNextId(),
                AccountId = accountId,
                Amount = amount,
                TransactionType = TransactionType.Withdraw,
                TransactionDate = DateTime.Now
            };

            bool resultWithdraw = _accountRepository.Update(existingAccount);

            if (!resultWithdraw)
                return false;

            bool resultTransaction = _transcationRepository.Add(transaction);

            if (!resultTransaction)
            {
                existingAccount.Balance += amount;
                _accountRepository.Update(existingAccount);
                return false;
            }

            return true;
        }

        public Account? GetAccountById(int id)
        {
            if (id <= 0) return null;
            return _accountRepository.GetById(id);
        }

        public List<Account> GetAccountsByCustomerId(int customerId)
        {
            if (customerId <= 0) return [];
            var accounts = _accountRepository.GetAccountsByCustomerId(customerId);
            return accounts;
        }

        public List<Account> GetAllAccounts()
        {
            return _accountRepository.GetAll();
        }

    }
}

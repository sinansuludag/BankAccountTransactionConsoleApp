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
    public class TranscationService : ITranscationService
    {
        private readonly ITranscationRepository _transcationRepository;

        public TranscationService(ITranscationRepository transcationRepository)
        {
            _transcationRepository = transcationRepository;
        }
        public List<Transaction> GetAllTransactions()
        {
            return _transcationRepository.GetAll();
        }

        public Transaction? GetTransactionById(int id)
        {
            if(id<=0) return null;
           return _transcationRepository.GetById(id);
        }

        public List<Transaction> GetTransactionsByAccountId(int accountId)
        {
            if (accountId <= 0) return [];
            return _transcationRepository.GetTransactionsByAccountId(accountId);
        }
    }
}

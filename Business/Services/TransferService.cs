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
    public class TransferService : ITransferService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransferRepository _transferRepository;
        private readonly ITranscationRepository _transcationRepository;

        public TransferService(IAccountRepository accountRepository, ITransferRepository transferRepository, ITranscationRepository transcationRepository)
        {
            _accountRepository = accountRepository;
            _transferRepository = transferRepository;
            _transcationRepository = transcationRepository;
        }
        public List<Transfer> GetAllTransfers()
        {
            return _transferRepository.GetAll();
        }

        public Transfer? GetTransferById(int id)
        {
            if (id <= 0)
                return null;

            return _transferRepository.GetById(id);
        }

        public bool MakeTransfer(Transfer transfer)
        {
            if (transfer == null) return false;
            if (transfer.Id <= 0) return false;
            if (transfer.SourceAccountId <= 0 ||
                transfer.DestinationAccountId <= 0) return false;
            if (transfer.Amount <= 0) return false;

            var sourceAccount = _accountRepository.GetById(transfer.SourceAccountId);
            if (sourceAccount == null) return false;

            var destinationAccount =
                _accountRepository.GetById(transfer.DestinationAccountId);

            if (destinationAccount == null) return false;

            if (sourceAccount.Id == destinationAccount.Id)
                return false;

            if (transfer.Amount > sourceAccount.Balance)
                return false;

            // Bakiyeleri değiştir
            sourceAccount.Balance -= transfer.Amount;
            destinationAccount.Balance += transfer.Amount;

            // Tarih
            transfer.TransferTarihi = DateTime.Now;

            // Transfer kaydını ekle
            bool transferResult = _transferRepository.Add(transfer);

            if (!transferResult)
            {
                sourceAccount.Balance += transfer.Amount;
                destinationAccount.Balance -= transfer.Amount;
                return false;
            }

            // Hesapları güncelle
            _accountRepository.Update(sourceAccount);
            _accountRepository.Update(destinationAccount);

            // Kaynak hesap hareketi
            Transaction sourceTransaction = new Transaction
            {  Id=_transcationRepository.GetNextId(),
                AccountId = sourceAccount.Id,
                Amount = transfer.Amount,
                TransactionType = TransactionType.Transfer,
                TransactionDate = DateTime.Now,
                SourceAccountId = sourceAccount.Id,
                DestinationAccountId = destinationAccount.Id
            };

            // Hedef hesap hareketi
            Transaction destinationTransaction = new Transaction
            {  Id= _transcationRepository.GetNextId(),
                AccountId = destinationAccount.Id,
                Amount = transfer.Amount,
                TransactionType = TransactionType.Transfer,
                TransactionDate = DateTime.Now,
                SourceAccountId = sourceAccount.Id,
                DestinationAccountId = destinationAccount.Id
            };

            bool sourceTransactionResult =
                _transcationRepository.Add(sourceTransaction);

            bool destinationTransactionResult =
                _transcationRepository.Add(destinationTransaction);

            if (!sourceTransactionResult || !destinationTransactionResult)
            {
                return false;
            }

            return true;
        }
    }
}

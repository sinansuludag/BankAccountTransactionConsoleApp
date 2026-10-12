using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITransferService
    {
        bool MakeTransfer(Transfer transfer);

        Transfer? GetTransferById(int id);

        List<Transfer> GetAllTransfers();
    }
}

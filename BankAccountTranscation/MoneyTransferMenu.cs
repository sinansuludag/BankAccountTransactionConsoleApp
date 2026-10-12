using Business.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTranscation
{
    public class MoneyTransferMenu
    {
        private readonly ITransferService _transferService;

        public MoneyTransferMenu(ITransferService transferService)
        {
            _transferService = transferService;
        }
        public void ShowMenu()
        {
            Console.WriteLine("PARA TRANSFER MENUSUNE HOSGELDINIZ !!!\n\n");

            while (true)
            {
                Console.WriteLine("Yapmak istediğiniz transfer işlemlerini aşağidaki menulerden birini secerek yapabilirsiniz.\n");
                Console.WriteLine("1 - Para Transferi Yap");
                Console.WriteLine("0 - Geri");

                Console.Write("Seçiminiz: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DoMoneyTransfer();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }

        }

        private void DoMoneyTransfer()
        {
            Console.WriteLine("Transfer id sini girebilir misiniz?");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Gecersiz bir id degeri girdiniz!!");
                return;
            }
            Console.Write("Para transferinin yapılacağı kaynak hesap ID'sini giriniz: ");
            if (!int.TryParse(Console.ReadLine(), out int sourceId))
            {
                Console.WriteLine("Gecersiz bir id degeri girdiniz!!");
                return;
            }
            Console.Write("Transfer yapılacak hedef hesap ID'sini giriniz: ");
            if (!int.TryParse(Console.ReadLine(), out int destinationId))
            {
                Console.WriteLine("Gecersiz bir id degeri girdiniz!!");
                return;
            }
            Console.Write("Transfer tutarını giriniz: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Gecersiz bir tutar girdiniz!!");
                return;
            }
            Console.WriteLine("Hangi transfer yontemi ile gonderim yapacaginizi asagidaki seceneklerden uygun birini secerek yapabilirsiniz.");
            Console.WriteLine("1 - Havale");
            Console.WriteLine("2 -EFT");
            Console.WriteLine("3 -FAST");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Gecersiz secim!!");
                return;
            }

            TransferType transferType;

            switch (choice)
            {
                case 1:
                    transferType = TransferType.Havale;
                    break;
                case 2:
                    transferType = TransferType.EFT;
                    break;
                case 3:
                    transferType = TransferType.Fast;
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    return;
            }

            Console.WriteLine("Transfer icin bir aciklama giriniz.");
            string? description= Console.ReadLine();

            Transfer transfer = new Transfer();
            transfer.Id = id;
            transfer.SourceAccountId = sourceId;
            transfer.DestinationAccountId = destinationId;
            transfer.Amount = amount;
            transfer.TransferType = transferType;
            transfer.Description = description;
            var result= _transferService.MakeTransfer(transfer);

            if (result)
                Console.WriteLine("Transfer isleminiz basariyla tamamlandı.");
            else
                Console.WriteLine("Transfer isleminiz gerceklesmedi!!");
        }
    }
}

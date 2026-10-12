using Business.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankAccountTranscation
{
    public class TranscationMenu
    {
        private readonly ITranscationService _transcationService;

        public TranscationMenu(ITranscationService transcationService)
        {
            _transcationService = transcationService;
        }
        public void ShowMenu()
        {
            Console.WriteLine("ISLEM HAREKETLERI MENUSUNE HOSGELDINIZ !!!\n\n");

            while (true)
            {
                Console.WriteLine("Görmek istediginiz islme hareketleri bilgileri için aşağidaki menulerden birini secebilirsiniz.\n");
                Console.WriteLine("1 - Hesap Hareketlerini Görüntüle");
                Console.WriteLine("2 - İşlem Detayını Islem Id ye göre Görüntüle");
                Console.WriteLine("3 - Tüm İşlemleri Görüntüle");
                Console.WriteLine("0 - Geri");

                Console.Write("Seçiminiz: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAccountMovements();
                        break;
                    case "2":
                        ShowTransactionById();
                        break;
                    case "3":
                        ShowAllTransaction();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }

        }


        private void ShowAllTransaction()
        {
           var result= _transcationService.GetAllTransactions();
            if (result.Count > 0)
            {
                foreach (var transaction in result)
                {
                    if (transaction.TransactionType == TransactionType.Transfer)
                    {
                        Console.WriteLine($"Islem ID={transaction.Id}\nTutar={transaction.Amount}\nIslem={TransactionType.Transfer}\n" +
                            $"Gönderen Hesap={transaction.SourceAccountId}\nAlici Hesap={transaction.DestinationAccountId}\nTarih={transaction.TransactionDate}\n");
                    }
                    else
                    {
                        Console.WriteLine($"Islem ID={transaction.Id}\nTutar={transaction.Amount}\nIslem={transaction.TransactionType}\n" +
                           $"Tarih={transaction.TransactionDate}\n");
                    }

                }
            }
            else
                Console.WriteLine("Henuz bir islem hareketi olusmadi.");
        }

        private void ShowTransactionById()
        {
            Console.WriteLine("Detayini görek istediginiz islem Id sini giriniz.");
            if(!int.TryParse(Console.ReadLine(),out int id))
            {
                Console.WriteLine("Gecersiz Id degeri girdiniz.");
                return;
            }
            var result = _transcationService.GetTransactionById(id);
            if (result != null)
            {
                if(result.TransactionType == TransactionType.Transfer)
                {
                    Console.WriteLine($"Islem ID={result.Id}\nHesap ID={result.AccountId}\nTutar={result.Amount}\nIslem={TransactionType.Transfer}\n" +
                           $"Gönderen Hesap={result.SourceAccountId}\nAlici Hesap={result.DestinationAccountId}\nTarih={result.TransactionDate}\n");
                }
                else
                {
                    Console.WriteLine($"Islem ID={result.Id}\nHesap ID= {result.AccountId}\nTutar={result.Amount}\nIslem={result.TransactionType}\n" +
                           $"Tarih={result.TransactionDate}\n");
                }
            }
            else
            {
                Console.WriteLine("Girilen islem Id ye ait bir islem bulunamadı.");
            }
        }

        private void ShowAccountMovements()
        {
            Console.WriteLine("Hesap id numarasini giriniz.");
            if(!int.TryParse(Console.ReadLine(), out int accountId))
            {
                Console.WriteLine("Gecersiz bir account id girdiniz.");
                return;
            }
            var result= _transcationService.GetTransactionsByAccountId(accountId);
            if(result.Count>0)
            {
                foreach (var transaction in result)
                {
                    if (transaction.TransactionType == TransactionType.Transfer)
                    {
                        Console.WriteLine($"Islem ID={transaction.Id}\nTutar={transaction.Amount}\nIslem={TransactionType.Transfer}\n" +
                            $"Gönderen Hesap={transaction.SourceAccountId}\nAlici Hesap={transaction.DestinationAccountId}\nTarih={transaction.TransactionDate}\n");
                    }
                    else
                    {
                        Console.WriteLine($"Islem ID={transaction.Id}\nTutar={transaction.Amount}\nIslem={transaction.TransactionType}\n" +
                           $"Tarih={transaction.TransactionDate}\n");
                    }

                }
            }
            else
                Console.WriteLine("Henuz bir islem hareketi olusmadi.");
        }
    }
}

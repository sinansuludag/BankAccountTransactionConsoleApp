using Business.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTranscation
{
    public class AccountMenu
    {
        private readonly IAccountService _accountService;

        public AccountMenu(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void ShowMenu()
        {
            Console.WriteLine("HESAP MENUSUNE HOSGELDINIZ !!!\n\n");

            while (true)
            {
                Console.WriteLine("Yapmak istediğiniz hesap işlemleri aşağidaki menulerden birini secerek yapabilirsiniz.\n");
                Console.WriteLine("1 - Hesap Oluştur");
                Console.WriteLine("2 - Hesap Sil");
                Console.WriteLine("3 - Hesap Getir");
                Console.WriteLine("4 - Müşterinin Hesaplarını Listele");
                Console.WriteLine("5 - Tüm Hesapları Listele");
                Console.WriteLine("6 - Para Yatır");
                Console.WriteLine("7 - Para Çek");
                Console.WriteLine("0 - Çıkış");

                Console.Write("Seçiminiz: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddAccount();
                        break;

                    case "2":
                        DeleteAccount();
                        break;

                    case "3":
                        GetAccount();
                        break;

                    case "4":
                        GetAllAccountsByCustomerId();
                        break;

                    case "5":
                        GetAllAccounts();
                        break;
                    case "6":
                        DepositMoney();
                        break;
                    case "7":
                        WithdrawMoney();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }

        }

        private void WithdrawMoney()
        {
            Console.WriteLine("Para cekmek istediginiz hesap id sini girebilir misiniz?");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Geçersiz bir ıd girdiniz!!");
                return;
            }
            Console.WriteLine("Hesabinizdan ne kadar para cekmek istiyorsunuz?");
            if (!decimal.TryParse(Console.ReadLine(), out decimal money))
            {
                Console.WriteLine("Geçersiz bir değer girdiniz!!");
                return;
            }
            var result = _accountService.Withdraw(id, money);
            if (result)
                Console.WriteLine($"{money} TL degerinde tutar {id} nolu hesabinizdan cekilmistir..");
            else
                Console.WriteLine("TL cekme isleminiz gerceklesmedi.");
        }

        private void DepositMoney()
        {
            Console.WriteLine("Para yatirmak istediginiz hesap id sini girebilir misiniz?");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Geçersiz bir ıd girdiniz!!");
                return;
            }
            Console.WriteLine("Hesabiniza ne kadar para yatirmak istiyorsunuz?");
            if (!decimal.TryParse(Console.ReadLine(), out decimal money))
            {
                Console.WriteLine("Geçersiz bir değer girdiniz!!");
                return;
            }
            var result = _accountService.Deposit(id,money);
            if (result)
                Console.WriteLine($"{money} TL degerindeki tutariniz {id} nolu hesabiniza yatirilmistir.");
            else
                Console.WriteLine("TL yatirma isleminiz gerceklesmedi.");
        }

        private void GetAllAccounts()
        {
            var result = _accountService.GetAllAccounts();

            if (result.Count == 0)
            {
                Console.WriteLine("Henuz bir hesap olusturulmadi.");
                return;
            }

            PrintAccounts(result);
        }

        private void GetAllAccountsByCustomerId()
        {
            Console.WriteLine("Hesaplarini getirmek istediginiz musteri ID'sini giriniz.");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Geçersiz ID girdiniz!!");
                return;
            }

            var result = _accountService.GetAccountsByCustomerId(id);

            if (result.Count == 0)
            {
                Console.WriteLine("Bu müşteriye ait hesap bulunamadı.");
                return;
            }

            PrintAccounts(result);
        }

        private void GetAccount()
        {
            Console.WriteLine("Getirmek istediginiz hesap id sini girebilir misiniz?");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Geçersiz ID girdiniz!!");
                return;
            }
            var result=_accountService.GetAccountById(id);
            if (result != null)
            {
                if(result is VadeliAccount vadeli)
                {
                    Console.WriteLine($"ID={vadeli.Id}\nHesap Numarasi={vadeli.AccountNumber}\nBakiye={vadeli.Balance}\nMusteri ID={vadeli.CustomerId}\nFaiz Orani={vadeli.FaizOrani}\nVade Baslangic Tarihi={vadeli.VadeBaslangicTarihi}\nVade Bitis Tarihi={vadeli.VadeBitisTarihi}\n");
                }
                else
                {
                    Console.WriteLine($"ID={result.Id}\nHesap Numarasi={result.AccountNumber}\nBakiye={result.Balance}\nMusteri ID={result.CustomerId}\n");
                }

            }
            else
                Console.WriteLine("Girdiginiz Id numarasina ait bir hesap bulunamadi.");
            
        }

        private void DeleteAccount()
        {
            Console.WriteLine("Silmek istediğiniz hesabın Id sini girebilir misiniz?");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Geçersiz ID girdiniz!!");
                return;
            }
            var result= _accountService.DeleteAccount(id);
            if (result)
                Console.WriteLine("Hesap basariyla silindi.");
            else
                Console.WriteLine("Hesap silme islemi gerceklesmedi!!");
        }

        private void AddAccount()
        {
            Console.WriteLine("Hesap ID sini girebilir misiniz?");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Geçersiz ID girdiniz!!");
                return;
            }
            Console.WriteLine("Hesap Numarasını girebilir misiniz?");
            if (!int.TryParse(Console.ReadLine(), out int hesapNo))
            {
                Console.WriteLine("Geçersiz numara girdiniz!!");
                return;
            }
            Console.WriteLine("Müşteri ID'sini giriniz:");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Geçersiz ID girdiniz!!");
                return;
            }
            Console.WriteLine("Vadeli hesap mı vadesiz hesap mı açmak istiyorsunuz?");
            Console.WriteLine("1 -Vadeli Hesap");
            Console.WriteLine("2 -Vadesiz Hesap");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Geçersiz karakter girdiniz!!");
                return;
            }
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Faiz oranını giriniz.");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal faiz))
                    {
                        Console.WriteLine("Geçersiz faiz oranı!");
                        return;
                    }
                    VadeliAccount accountVadeli = new VadeliAccount();
                      accountVadeli.Id = id;
                      accountVadeli.AccountNumber = hesapNo;
                    accountVadeli.Balance = 0;
                    accountVadeli.CustomerId = customerId;
                    accountVadeli.VadeBaslangicTarihi=DateTime.Now;
                    accountVadeli.VadeBitisTarihi = DateTime.Now.AddDays(30);
                    accountVadeli.FaizOrani = faiz;
                   var result= _accountService.AddAccount(accountVadeli);
                    if (result)
                        Console.WriteLine("Hesap başarılı bir şekilde oluşturuldu.");
                    else
                        Console.WriteLine("Hesap oluşturulmadı.");
                    break;
                 case 2:
                    VadesizAccount accountVadesiz = new VadesizAccount();
                    accountVadesiz.Id = id;
                    accountVadesiz.AccountNumber = hesapNo;
                    accountVadesiz.Balance = 0;
                    accountVadesiz.CustomerId = customerId;
                    
                    var result1 = _accountService.AddAccount(accountVadesiz);
                    if (result1)
                        Console.WriteLine("Hesap başarılı bir şekilde oluşturuldu.");
                    else
                        Console.WriteLine("Hesap oluşturulmadı.");
                    break;
                default:
                    Console.WriteLine("Geçersiz hesap türü seçtiniz.");
                    break;
            }

            
        }

        private void PrintAccounts(List<Account> accounts)
        {
            foreach (var account in accounts)
            {
                if (account is VadeliAccount vadeli)
                {
                    Console.WriteLine(
                        $"ID={vadeli.Id}\n" +
                        $"Hesap Numarasi={vadeli.AccountNumber}\n" +
                        $"Bakiye={vadeli.Balance}\n" +
                        $"Musteri ID={vadeli.CustomerId}\n" +
                        $"Faiz Orani={vadeli.FaizOrani}\n" +
                        $"Vade Baslangic Tarihi={vadeli.VadeBaslangicTarihi}\n" +
                        $"Vade Bitis Tarihi={vadeli.VadeBitisTarihi}\n");
                }
                else
                {
                    Console.WriteLine(
                        $"ID={account.Id}\n" +
                        $"Hesap Numarasi={account.AccountNumber}\n" +
                        $"Bakiye={account.Balance}\n" +
                        $"Musteri ID={account.CustomerId}\n");
                }
            }
        }
    }
    }


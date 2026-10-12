


using BankAccountTranscation;
using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Data.Repositories;

class Program
{
    private static ICustomerRepository _customerRepository =
      new CustomerRepository();

    private static IAccountRepository _accountRepository =
        new AccountRepository();
    private static ITransferRepository _transferRepository = new TransferRepository();
    private static ITranscationRepository _transcationRepository= new TransactionRepository();


    // 2️⃣ Service'leri BİR KERE oluşturuyoruz
    private static ICustomerService _customerService =
        new CustomerService(_customerRepository);

    private static IAccountService _accountService =
        new AccountService(
            _accountRepository,
            _customerRepository, _transcationRepository);
    private static ITransferService _transferService= new TransferService(_accountRepository, _transferRepository,_transcationRepository);
    private static ITranscationService _transactionService = new TranscationService(_transcationRepository);
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("BANKA UYGULAMASINA HOSGELDINIZ !!!\n");
            Console.WriteLine("1 - Müşteri İşlemleri");
            Console.WriteLine("2 - Hesap İşlemleri");
            Console.WriteLine("3 - Para Transfer İşlemleri");
            Console.WriteLine("4 - Transfer Hareketleri");
            Console.WriteLine("0 - Çıkış");

            Console.Write("\nSeçiminiz: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CustomerTranscation();
                    break;

                case "2":
                    AccountTranscation();
                    break;

                case "3":
                    MoneyTransfer();
                    break;

                case "4":
                    TransferMovements();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        }
    }

    private static void TransferMovements()
    {
       TranscationMenu transcationMenu= new TranscationMenu(_transactionService);
        transcationMenu.ShowMenu();
    }

    private static void MoneyTransfer()
    {
        MoneyTransferMenu moneyTransferMenu = new MoneyTransferMenu(_transferService);
        moneyTransferMenu.ShowMenu();
    }

    private static void AccountTranscation()
    {
       
        AccountMenu accountMenu = new AccountMenu(_accountService);
        accountMenu.ShowMenu();
    }

    private static void CustomerTranscation()
    { 
        CustomerMenu customer = new CustomerMenu(_customerService);
        customer.ShowMenu();

    }
}

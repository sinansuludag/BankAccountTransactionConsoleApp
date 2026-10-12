using Business.Interfaces;
using Entities;
using System;


namespace BankAccountTranscation
{
    public class CustomerMenu
    {
        private readonly ICustomerService _customerService;

        public CustomerMenu(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void ShowMenu()
        {
            Console.WriteLine("MUSTERI MENUSUNE HOSGELDINIZ !!!\n\n");

            while (true)
            {             
               Console.WriteLine("Yapmak istediğiniz müşter işlemleri aşağidaki menulerden birini secerek yapabilirsiniz.\n");
                Console.WriteLine("1 - Müşteri Ekle");
                Console.WriteLine("2 - Müşteri Sil");
                Console.WriteLine("3 - Müşteri Güncelle");
                Console.WriteLine("4 - Müşteri Getir");
                Console.WriteLine("5 - Tüm Müşterileri Listele");
                Console.WriteLine("0 - Çıkış");

                Console.Write("Seçiminiz: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCustomer();
                        break;

                    case "2":
                        DeleteCustomer();
                        break;

                    case "3":
                        UpdateCustomer();
                        break;

                    case "4":
                        GetCustomer();
                        break;

                    case "5":
                        GetAllCustomers();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }

        }

        private void GetAllCustomers()
        {

            var customers = _customerService.GetAllCustomers();

            if (customers.Count == 0)
            {
                Console.WriteLine("Sistemde kayıtlı müşteri bulunmamaktadır.");
                return;
            }

            foreach (var customer in customers)
            {
                Console.WriteLine(
                    $"Müşteri ID = {customer.Id}\n" +
                    $"Ad = {customer.FirstName}\n" +
                    $"Soyad = {customer.LastName}\n" +
                    $"TC = {customer.Tc}\n" +
                    $"Telefon No = {customer.Phone}\n"
                );
            }
        }

        private void GetCustomer()
        {
            Console.WriteLine("Getirmek istediğiniz müsteri ID sini giriniz.");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Geçerli bir ID giriniz.");
                return;
            }
            var result = _customerService.GetCustomerById(id);
            if (result != null)
            {
                Console.WriteLine($"ID: {result.Id}");
                Console.WriteLine($"Ad: {result.FirstName}");
                Console.WriteLine($"Soyad: {result.LastName}");
                Console.WriteLine($"TC: {result.Tc}");
                Console.WriteLine($"Telefon: {result.Phone}");
            }
            else
            {
                Console.WriteLine("Bu ID'ye sahip müşteri bulunamadı.");
            }
        }

        private void UpdateCustomer()
        {
            Console.WriteLine("Güncellemek istediğiniz musteri ID sini giriniz.");
            if (!int.TryParse(Console.ReadLine(),out int id))
            {
                Console.WriteLine("Geçerli bir ID giriniz.");
                return;
            }
            Console.WriteLine("Musteri adını girebilir misiniz?");
            string firstName = Console.ReadLine();
            Console.WriteLine("Musteri soyadini girebilir misiniz?");
            string lastName = Console.ReadLine();
            Console.WriteLine("Musteri tc sini girebilir misiniz?");
            string tc = Console.ReadLine();
            Console.WriteLine("Musteri telefon numarasını girebilir misiniz?");
            string phone = Console.ReadLine();

            Customer customer = new Customer
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Tc = tc,
                Phone = phone
            };

            var result=_customerService.UpdateCustomer(customer);
            if (result)
                Console.WriteLine("Guncelleme islemi basarili.");
            else
                Console.WriteLine("Guncelleme islemi gerceklesmedi.");

        }

        private void DeleteCustomer()
        {
            Console.WriteLine("Silmek istediğiniz musteri ID sini giriniz.");
            if (!int.TryParse(Console.ReadLine(),out int id))
            {
                Console.WriteLine("Geçerli bir ID giriniz.");
                return;
            }
            var result = _customerService.DeleteCustomer(id);
            if (result)
                Console.WriteLine("Musteri basariyla silindi.");
            else
                Console.WriteLine("Musteri silme islemi gerceklesmedi.");
        }

        private void AddCustomer()
        {
            Console.WriteLine("Musteri ID sini girebilir misiniz?");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Geçerli bir ID giriniz.");
                return;
            }
            Console.WriteLine("Musteri adını girebilir misiniz?");
            string firstName=Console.ReadLine();
            Console.WriteLine("Musteri soyadini girebilir misiniz?");
            string lastName = Console.ReadLine(); 
            Console.WriteLine("Musteri tc sini girebilir misiniz?");
            string tc = Console.ReadLine();
            Console.WriteLine("Musteri telefon numarasını girebilir misiniz?");
            string phone = Console.ReadLine();

            Customer customer = new Customer
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Tc = tc,
                Phone = phone
            };


            var result= _customerService.AddCustomer(customer);
            if (result)
                Console.WriteLine("Müşteri başarıyla eklendi.\n\n");
            else
            Console.WriteLine("Müşteri ekleme işlemi gerçekleşmedi!!");
        }
    }
}

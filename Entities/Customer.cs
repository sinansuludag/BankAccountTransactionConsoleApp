using Entities.Interfaces;

namespace Entities
{
    public class Customer: IEntity
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Tc { get; set; }
        public required string Phone { get; set; }
        public  List<Account> Accounts { get; set; } = new List<Account>();

        public void WithCustomer(
     string? firstName = null,
     string? lastName = null,
     string? tc = null,
     string? phone = null)
        {
            if (firstName != null)
                FirstName = firstName;

            if (lastName != null)
                LastName = lastName;

            if (tc != null)
                Tc = tc;

            if (phone != null)
                Phone = phone;
        }

    }
}

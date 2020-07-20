using System;

namespace Account.Data.Entites
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PassowrdSalt { get; set; }
    }
}

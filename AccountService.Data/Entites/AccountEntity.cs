using System;

namespace Account.Data.Entites
{
    public class AccountEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public virtual CustomerEntity  Customer { get; set; }
        public DateTime OpenDate { get; set; }
        public int Balance { get; set; }
    }
}

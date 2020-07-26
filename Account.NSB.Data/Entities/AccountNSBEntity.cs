using System;
using System.Collections.Generic;
using System.Text;

namespace Account.NSB.Data.Entities
{
   public class AccountNSBEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
       // public virtual CustomerEntity Customer { get; set; }
        public DateTime OpenDate { get; set; }
        public int Balance { get; set; }
    }
}

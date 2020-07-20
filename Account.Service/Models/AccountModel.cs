using System;

namespace Account.Service.Models
{
    public class AccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime OpenDate { get; set; }
        public float Balance { get; set; }
    }
}

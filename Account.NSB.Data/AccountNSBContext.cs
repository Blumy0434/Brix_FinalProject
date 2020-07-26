using Account.NSB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.NSB.Data
{
    public class AccountNSBContext : DbContext
    {
        public AccountNSBContext(DbContextOptions<AccountNSBContext> options) : base(options)
        {

        }

        public AccountNSBContext()
        { }

        public DbSet<AccountNSBEntity> Accounts { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }    
}

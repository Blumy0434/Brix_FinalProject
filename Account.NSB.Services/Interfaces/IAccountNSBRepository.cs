using Messages.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.NSB.Service.Interfaces
{
    public interface IAccountNSBRepository
    {
        Task<bool> CheckAccountsCorrectness(Guid fromAccountId, Guid toAccountId);
        Task<bool> CheckBalance(Guid fromAccountId, int amount);
        Task<bool> UpdateAccounts(UpdateAccounts message);
    }
}

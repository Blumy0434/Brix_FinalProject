using Account.Data;
using Account.NSB.Service.Interfaces;
using Messages.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Account.NSB.Data
{
    public class AccountNSBRepository : IAccountNSBRepository
    {
        private readonly AccountContext _accountContext;

        public AccountNSBRepository(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }
        public async Task<bool> CheckAccountsCorrectness(Guid fromAccountId, Guid toAccountId)
        {
            try
            {
                var fromAccount = await _accountContext.Accounts
                .FirstOrDefaultAsync(account => account.Id == fromAccountId);
                var toAccount = await _accountContext.Accounts
                    .FirstOrDefaultAsync(account => account.Id == toAccountId);
                if (fromAccount == null || toAccount == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                throw new SystemException();
            }

        }

        public async Task<bool> CheckBalance(Guid fromAccountId, int amount)
        {
            try
            {
                var account = await _accountContext.Accounts
                .FirstOrDefaultAsync(account => account.Id == fromAccountId);

                if (account.Balance < amount)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                throw new SystemException();
            }
        }

        public async Task<bool> UpdateAccounts(UpdateAccounts message)
        {
            try
            {
                var fromAccount = await _accountContext.Accounts
                .FirstOrDefaultAsync(account => account.Id == message.FromAccountId);

                fromAccount.Balance -= message.Amount;

                var toAccount = await _accountContext.Accounts
                    .FirstOrDefaultAsync(account => account.Id == message.ToAccountId);

                toAccount.Balance += message.Amount;

                await _accountContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new SystemException();
            }
        }
    }
}

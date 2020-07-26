using Account.NSB.Service.Interfaces;
using Messages.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.NSB.Service
{
    public class AccountNSBService: IAccountNSBService
    {
        private readonly IAccountNSBRepository _accountNSBRepository;

        public AccountNSBService(IAccountNSBRepository accountNSBRepository)
        {
            _accountNSBRepository = accountNSBRepository;
        }

        public async Task<bool> UpdateAccounts(UpdateAccounts message)
        {
            bool areAccountsExist = await _accountNSBRepository
                .CheckAccountsCorrectness(message.FromAccountId, message.ToAccountId);
            if(areAccountsExist == false)
            {
                return false;
            }
            bool isBalanceEnough = await _accountNSBRepository
                .CheckBalance(message.FromAccountId, message.Amount);
            if (isBalanceEnough == false)
            {
                return false;
            }
            return await _accountNSBRepository.UpdateAccounts(message);
        }
    }
}

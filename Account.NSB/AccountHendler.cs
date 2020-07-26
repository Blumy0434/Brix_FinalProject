using Account.NSB.Service.Interfaces;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using System.Threading.Tasks;

namespace Account.NSB
{
    public class AccountHendler : IHandleMessages<UpdateAccounts>
    {
        private readonly IAccountNSBService _accountNSBService;

        public AccountHendler(IAccountNSBService accountNSBService)
        {
            _accountNSBService = accountNSBService;
        }
        public async Task Handle(UpdateAccounts message, IMessageHandlerContext context)
        {
            AccountsUpdated accountsUpdated = new AccountsUpdated
            {
                TransactionId = message.TransactionId
            };
            bool isAccountsUpdated = await _accountNSBService.UpdateAccounts(message);
            if (isAccountsUpdated)
            {
                accountsUpdated.isAccountsUpdateSuccess = true;
            }
            else
            {
                accountsUpdated.isAccountsUpdateSuccess = false;
            }
            await context.Publish(accountsUpdated)
                .ConfigureAwait(false);
        }
    }
}

using Messages.Commands;
using System.Threading.Tasks;

namespace Account.NSB.Service.Interfaces
{
    public interface IAccountNSBService
    {
        Task<bool> UpdateAccounts(UpdateAccounts message);
    }
}

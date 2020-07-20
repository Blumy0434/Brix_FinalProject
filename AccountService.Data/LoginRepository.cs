using Account.Data.Entites;
using Account.Service.Intefaces;
using Account.Service.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Account.Data
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AccountContext _accountContext;
        private readonly IMapper _mapper;

        public LoginRepository(AccountContext accountContext, IMapper mapper)
        {
            _accountContext = accountContext;
            _mapper = mapper;
        }

        public async Task<bool> IsEmailValidAsync(string email)
        {
            CustomerEntity customerEntity = await _accountContext.Customers
                                                .FirstOrDefaultAsync(s => s.Email == email);
            if (customerEntity != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsCustomerExistAsync(string email, string password)
        {
            CustomerEntity customer = await _accountContext.Customers
                    .FirstOrDefaultAsync(c => c.Email == email
                                           && c.Password == password);
            if (customer != null)
            {
                return true;
            }
            return false;
        }

        public async Task<Guid> LoginAsync(string email, string password)
        {
            //try
            //{
            //    var customer = await _accountContext.Customers
            //        .FirstOrDefaultAsync(c => c.Email == email
            //                               && c.Password == password);

            //    if (customer != null)
            //    {
            //        return customer.Id;
            //    }
            //    else
            //    {
            //        return default;
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw new Exception();
            //}
            try
            {
                CustomerEntity customer = await _accountContext.Customers
                     .FirstOrDefaultAsync(c => c.Email == email
                                            && c.Password == password);
                return customer.Id;
            }
            catch
            {
                throw new SystemException();
            }
        }

        public async Task<bool> RegisterAsync(CustomerModel customerModel, AccountRegisterModel accountRegisterModel)
        {
            try
            {
                CustomerEntity customer = _mapper.Map<CustomerEntity>(customerModel);
                AccountEntity account = _mapper.Map<AccountEntity>(accountRegisterModel);
                await _accountContext.Customers.AddAsync(customer);
                await _accountContext.Accounts.AddAsync(account);
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

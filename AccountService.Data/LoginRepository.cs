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
        /*
         
        public async Task<bool> IsCustomerExistAsync(string email, string password)
        {
            CustomerEntity customer = await _accountContext.Customers
             .FirstOrDefaultAsync(c => c.Email == email);
            if (customer != null)
            {
                byte[] passwordHash, passwordSalt;
                passwordHash = customer.PasswordHash;
                passwordSalt = customer.PassowrdSalt;
                if (VerifyPassword(password, passwordHash, passwordSalt))
                    return true;
                return false;
            }
            else
                return false;
        }

        public async Task<Guid> LoginAsync(string email)
        {
            try
            {
                CustomerEntity customer = await _accountContext.Customers
             .FirstOrDefaultAsync(c => c.Email == email);
                return customer.Id;
            }
            catch (Exception)
            {
                throw new SystemException();
            }
        }
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<bool> RegisterAsync(CustomerModel customerModel, AccountRegisterModel accountRegisterModel)
        {
            byte[] passwordHash, passwordSalt;
            string passowrd = customerModel.Password;
            CreatePasswordHash(passowrd, out passwordHash, out passwordSalt);
            try
            {
                //  CustomerEntity customer = _mapper.Map<CustomerEntity>(customerModel);
                CustomerEntity customer = new CustomerEntity()
                {
                    PasswordHash = passwordHash,
                    PassowrdSalt = passwordSalt,
                    Email = customerModel.Email,
                    FirstName = customerModel.FirstName,
                    LastName = customerModel.LastName,
                    Id = customerModel.Id
                };
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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }*/
    }
}

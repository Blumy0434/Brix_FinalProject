using Account.Service.Intefaces;
using Account.Service.Models;
using Exceptions;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Account.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }



        public async Task<Guid> LoginAsync(string email, string password)
        {
            //bool passwordAsHashCode = VerifyHashedPassword(password);
            bool isCustomerExist = await _loginRepository.IsCustomerExistAsync(email, password /*passwordAsHashCode*/);
            if (isCustomerExist)
            {
                return await _loginRepository.LoginAsync(email, password/*passwordAsHashCode*/);
            }
            else
            {
                throw new AccountNotFoundException();
            }
        }

        public async Task<bool> RegisterAsync(CustomerModel customerModel)
        {
            bool isEmailValid = await _loginRepository.IsEmailValidAsync(customerModel.Email);
            if (isEmailValid)
            {
                customerModel.Id = Guid.NewGuid();
                //customerModel.Password = HashPassword(customerModel.Password);
                AccountRegisterModel account = new AccountRegisterModel
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerModel.Id,
                    Balance = 1000,
                    OpenDate = DateTime.Now
                };
                return await _loginRepository.RegisterAsync(customerModel, account);
            }
            else
            {
                throw new DuplicateEmailException();
            }
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        //public static bool VerifyHashedPassword(string hashedPassword, string password)
        //{
        //    byte[] buffer4;
        //    if (hashedPassword == null)
        //    {
        //        return false;
        //    }
        //    if (password == null)
        //    {
        //        throw new ArgumentNullException("password");
        //    }
        //    byte[] src = Convert.FromBase64String(hashedPassword);
        //    if ((src.Length != 0x31) || (src[0] != 0))
        //    {
        //        return false;
        //    }
        //    byte[] dst = new byte[0x10];
        //    Buffer.BlockCopy(src, 1, dst, 0, 0x10);
        //    byte[] buffer3 = new byte[0x20];
        //    Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
        //    using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
        //    {
        //        buffer4 = bytes.GetBytes(0x20);
        //    }
        //    return ByteArraysEqual(buffer3, buffer4);
        //}
    }
}

﻿using Account.Service.Intefaces;
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
            bool isCustomerExist = await _loginRepository.IsCustomerExistAsync(email, password);
            if (isCustomerExist)
            {
                return await _loginRepository.LoginAsync(email, password);
            }
            else
            {
                throw new CustomerNotFoundException();
            }
        }

        public async Task<bool> RegisterAsync(CustomerModel customerModel)
        {
            bool isEmailValid = await _loginRepository.IsEmailValidAsync(customerModel.Email);
            if (isEmailValid)
            {
                customerModel.Id = Guid.NewGuid();               
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
    }
}

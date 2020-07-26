﻿using Account.Service.Models;
using System;
using System.Threading.Tasks;

namespace Account.Service.Intefaces
{
    public interface ILoginRepository
    {        
        Task<Guid> LoginAsync(string email);
        //Task<Guid> LoginAsync(string email, string password);
        Task<bool> IsEmailValidAsync(string email);
        Task<bool> IsCustomerExistAsync(string email, string password);
        Task<bool> RegisterAsync(CustomerModel customerModel, AccountRegisterModel account);
    }
}

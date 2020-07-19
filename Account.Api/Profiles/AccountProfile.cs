﻿using Account.Service.Models;
using Account.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Account.Data.Entites;

namespace Account.WebApi.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountModel, AccountDTO>();
            CreateMap<AccountDTO, AccountModel>();
            CreateMap<AccountModel, AccountEntity>();
            CreateMap<AccountEntity, AccountModel>();
        }
    }
}
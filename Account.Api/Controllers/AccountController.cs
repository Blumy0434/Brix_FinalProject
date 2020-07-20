using Account.Service.Intefaces;
using Account.Service.Models;
using Account.WebApi.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Account.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService,IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        [HttpGet("GetAccountInfo")]
        public async Task<AccountDTO> Get([FromQuery]string guid)
        {
            Guid guid1 = Guid.Parse(guid);        
            AccountModel accountModel = await _accountService.GetAccountInfoAsync(guid1);
            return _mapper.Map<AccountDTO>(accountModel);

        }
    }
}
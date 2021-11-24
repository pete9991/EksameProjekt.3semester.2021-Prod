using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morales.BookingSystem.Dtos.Accounts;

namespace Morales.BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountservice;

        public AccountController(IAccountService accountService)
        {
            _accountservice = accountService;
        }
        [HttpGet]
        public ActionResult<AccountsDto> GetAll()
        {
            try
            {
                var accounts = _accountservice.GetAll()
                    .Select(p => new AccountDto()
                    {
                        Id = p.Id,
                        Type = p.Type,
                        Name = p.Name,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Sex = p.Sex
                        
                    })
                    .ToList();
                return Ok(new AccountsDto()
                {
                    AccountList = accounts
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<AccountDto> GetAccount(int id)
        {
            try
            {
                var account = _accountservice.GetAccount(id);
                var dto = new AccountDto
                {
                    Email = account.Email,
                    Id = account.Id,
                    Name = account.Name,
                    PhoneNumber = account.PhoneNumber,
                    Sex = account.Sex,
                    Type = account.Type
                };
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<AccountDto> DeleteAccount(int id)
        {
            try
            {
                var account = _accountservice.DeleteAccount(id);
                var dto = new AccountDto
                {
                    Email = account.Email,
                    Id = account.Id,
                    Name = account.Name,
                    PhoneNumber = account.PhoneNumber,
                    Sex = account.Sex,
                    Type = account.Type
                };
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public ActionResult<AccountDto> CreateAccount([FromBody] AccountDto accountDto)
        {
            try
            {
                var accountToCreate = new Account()
                {
                    Email = accountDto.Email,
                    Name = accountDto.Name,
                    PhoneNumber = accountDto.PhoneNumber,
                    Sex = accountDto.Sex,
                    Type = accountDto.Type
                };
                var accountCreated = _accountservice.CreateAccount(accountToCreate);
                return Created($"https://localhost/api/Product/{accountCreated.Id}", accountCreated);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morales.BookingSystem.Dtos.Accounts;
using Morales.BookingSystem.PolicyHandlers;
using Morales.BookingSystem.Security;
using Morales.BookingSystem.Security.Models;

namespace Morales.BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountservice;
        private readonly IAuthService _authService;

        public AccountController(IAccountService accountService, IAuthService authService)
        {
            _accountservice = accountService;
            _authService = authService;
        }
        [Authorize(Policy = nameof(EmployeeHandler))]
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

        [Authorize(Policy = nameof(CustomerHandler))]
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

        //Not sure if admin of customer, should be diccussed
        [Authorize(Policy = nameof(CustomerHandler))]
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

        //we should probably make sepearte methods for creating admins/employees/customers
        [AllowAnonymous]
        [HttpPost(nameof(CreateAccount))]
        public ActionResult<AccountDto> CreateAccount([FromBody] AccountDto accountDto)
        {
            try
            {
                var accountToCreate = new Account
                {
                    Email = accountDto.Email,
                    Name = accountDto.Name,
                    PhoneNumber = accountDto.PhoneNumber,
                    Sex = accountDto.Sex,
                    Type = accountDto.Type
                };
                var authAccountToCreate = new LoginUser
                {
                    UserName = accountDto.PhoneNumber,
                    HashedPassword = accountDto.Password
                };
                var accountCreated = _accountservice.CreateAccount(accountToCreate);
                _authService.CreateNewAccount(authAccountToCreate, _accountservice.GetAccountFromPhoneNumber(accountDto.PhoneNumber));
                _authService.EstablishPermissions(accountToCreate);
                return Created($"https://localhost/api/Account/{accountCreated.Id}", accountCreated);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Policy = nameof(CustomerHandler))]
        [HttpPut("{id}")]
        public ActionResult<AccountDto> UpdateAccount(int id, [FromBody] AccountDto accountToUpdate)
        {
            try
            {
                if (id != accountToUpdate.Id)
                {
                    return BadRequest("ID in parameters must the same as in object");
                }

                return Ok(_accountservice.UpdateAccount(new Account()
                {
                    Email = accountToUpdate.Email,
                    Id = accountToUpdate.Id,
                    Name = accountToUpdate.Name,
                    PhoneNumber = accountToUpdate.PhoneNumber,
                    Sex = accountToUpdate.Sex,
                    Type = accountToUpdate.Type
                }));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
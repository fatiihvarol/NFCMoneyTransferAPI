using Microsoft.AspNetCore.Mvc;
using NFCMoneyTransferAPI.DTOs;
using NFCMoneyTransferAPI.Services.AccountService;
using System.Collections.Generic;
using System.Threading.Tasks;
using NFCMoneyTransferWebAPI.Services.AccountService;

namespace NFCMoneyTransferAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] CreateAccountDto createAccountDto)
        {
            var account = await _accountService.AddAccountAsync(createAccountDto);
            return CreatedAtAction(nameof(GetAccountById), new { accountId = account.AccountID }, account);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccountById(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null) return NotFound();

            return Ok(account);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            await _accountService.DeleteAccountAsync(accountId);
            return NoContent();
        }
    }
}
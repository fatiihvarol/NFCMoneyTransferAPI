using NFCMoneyTransferAPI.DTOs;
using NFCMoneyTransferAPI.Entity;

namespace NFCMoneyTransferWebAPI.Services.AccountService;


public interface IAccountService
{
    Task<AccountDto> AddAccountAsync(CreateAccountDto createAccountDto);
    Task<IEnumerable<AccountDto>>GetAccountByIdAsync(int accountId);
    Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
    Task DeleteAccountAsync(int accountId);
}

using NFCMoneyTransferAPI.DTOs;

namespace NFCMoneyTransferAPI.Services.TransactionService;


    public interface ITransactionService
    {
        Task<TransactionDto> TransferFundsAsync(int fromAccountId, int toAccountId, decimal amount);
    }


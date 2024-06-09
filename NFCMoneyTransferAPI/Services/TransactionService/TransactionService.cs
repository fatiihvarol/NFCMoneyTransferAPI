using Microsoft.EntityFrameworkCore;
using NFCMoneyTransferAPI.DbContext;
using NFCMoneyTransferAPI.DTOs;
using NFCMoneyTransferAPI.Entity;

namespace NFCMoneyTransferAPI.Services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionDto> TransferFundsAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountID == fromAccountId);
            var toAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountID == toAccountId);

            if (fromAccount == null || toAccount == null)
            {
                throw new ArgumentException("Account not found.");
            }

            if (fromAccount.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            var transaction = new Transaction
            {
                Amount = amount,
                Date = DateTime.UtcNow,
                FromAccountID = fromAccountId,
                FromAccount = fromAccount,
                ToAccountID = toAccountId,
                ToAccount = toAccount
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return new TransactionDto
            {
                TransactionID = transaction.TransactionID,
                Amount = transaction.Amount,
                Date = transaction.Date,
                FromAccountID = transaction.FromAccountID,
                ToAccountID = transaction.ToAccountID
            };
        }
    }
}
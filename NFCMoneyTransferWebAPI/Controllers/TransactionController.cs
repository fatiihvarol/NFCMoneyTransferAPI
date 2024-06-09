using Microsoft.AspNetCore.Mvc;

using NFCMoneyTransferAPI.Services.TransactionService;

namespace NFCMoneyTransferAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> TransferFunds([FromBody] TransferRequestDto request)
        {
            try
            {
                var transaction = await _transactionService.TransferFundsAsync(request.FromAccountID, request.ToAccountID, request.Amount);
                return Ok(transaction);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class TransferRequestDto
    {
        public int FromAccountID { get; set; }
        public int ToAccountID { get; set; }
        public decimal Amount { get; set; }
    }
}
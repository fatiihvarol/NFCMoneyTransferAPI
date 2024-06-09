namespace NFCMoneyTransferAPI.DTOs;

public class TransactionDto
{
    public int TransactionID { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int FromAccountID { get; set; }
    public int ToAccountID { get; set; }
}
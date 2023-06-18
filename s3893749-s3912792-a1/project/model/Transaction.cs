namespace s3893749_s3912792_a1.project.model;

public class Transaction
{
    public int TransactionId { get; set; }
    public char TransactionType { get; set; }
    public int AccountNumber { get; set; }
    public int DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }  
    public string Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }
}
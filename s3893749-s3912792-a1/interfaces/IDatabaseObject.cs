namespace s3893749_s3912792_a1.interfaces;

public interface IDatabaseObject
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }

    public string PostCode { get; set; }
    public int AccountNumber { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
    
    public int TransactionId { get; set; }
    public char TransactionType { get; set; }
    public int DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }  
    public string Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }
}
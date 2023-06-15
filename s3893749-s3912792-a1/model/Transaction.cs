using System.Data.SqlTypes;
using s3893749_s3912792_a1.interfaces;

namespace s3893749_s3912792_a1.model;

public class Transaction:IDatabaseObject
{
    
    public int TransactionId { get; set; }
    public char TransactionType { get; set; }
    public int AccountNumber { get; set; }
    public int DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }  
    public string Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }
    public int CustomerId { get; set; }
}
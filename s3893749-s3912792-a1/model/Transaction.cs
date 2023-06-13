using System.Data.SqlTypes;

namespace s3893749_s3912792_a1.model;

public class Transaction
{
    /*private decimal _amount;
    private string _comment;
    private DateTime _transactionTimeUtc;
    private int _accountNumber;

    public Transaction(int accountNumber, decimal amount, DateTime datetime, string comment)
    {
        _accountNumber = accountNumber;
        _amount = amount;
        _transactionTimeUtc = datetime;
        _comment = comment;
    }*/
    
    public int TransactionId { get; set; }
    public char TransactionType { get; set; }
    public int AccountNumber { get; set; }
    public int DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }  
    public string Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }
  
    
}
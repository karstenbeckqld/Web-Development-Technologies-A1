using System.Data.SqlTypes;

namespace s3893749_s3912792_a1.model;

public class Transaction
{
    private SqlMoney _amount;
    private string _comment;
    private DateTime _transactionTime;
    private int _accountNumber;

    public Transaction(int accountNumber, decimal amount, DateTime datetime, string comment)
    {
        _accountNumber = accountNumber;
        _amount = amount;
        _transactionTime = datetime;
        _comment = comment;
    }
    
    public int AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionTime { get; set; }
    public string Comment { get; set; }
    
}
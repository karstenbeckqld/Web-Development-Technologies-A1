namespace A1ClassLibrary.model;

public class Transaction
{

    /*private int _transactionId;
    private string _transactionType;
    private int _accountNumber;
    private int _destinationAccountNumber;
    private decimal _amount;
    private string _comment;
    private DateTime _transactionTimeUtc;

    public Transaction(int transactionId, string transactionType, int accountNumber, int destinationAccountNumber,
        decimal amount, string comment, DateTime transactionTimeUtc)
    {
        _transactionId = transactionId;
        _transactionType = transactionType;
        _accountNumber = accountNumber;
        _destinationAccountNumber = destinationAccountNumber;
        _amount = amount;
        _comment = comment;
        _transactionTimeUtc = transactionTimeUtc;
    }*/
    
    public int TransactionID { get; set; }
    public char TransactionType { get; set; }
    public int AccountNumber { get; set; }
    public int? DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }  
    public string Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }

    public override string ToString()
    {
        return $"TransactionID: {TransactionID}\n " +
               $"TransactionType: {TransactionType}\n " +
               $"AccountNumber: {AccountNumber}\n " +
               $"DestinationAccountNumber: {DestinationAccountNumber}\n " +
               $"Amount: {Amount}\n " +
               $"Comment: {Comment}\n " +
               $"TransactionTimeUtc: {TransactionTimeUtc}";
    }
}
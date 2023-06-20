namespace A1ClassLibrary.model;

public class Transaction
{
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
namespace A1ClassLibrary.model;

public class Transaction
{
    public int TransactionID { get; set; }
    public required char TransactionType { get; set; }
    public required int AccountNumber { get; set; }
    public int? DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }  
    public string Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }

    public override string ToString()
    {
        return $"TransactionID: {TransactionID} - " +
               $"TransactionType: {TransactionType} - " +
               $"AccountNumber: {AccountNumber} - " +
               $"DestinationAccountNumber: {DestinationAccountNumber} - " +
               $"Amount: {Amount} - " +
               $"Comment: {Comment} - " +
               $"TransactionTimeUtc: {TransactionTimeUtc}";
    }
}
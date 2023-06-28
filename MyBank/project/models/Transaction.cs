using System.Diagnostics.CodeAnalysis;

namespace MyBank.project.models;

// The Transaction class represents a user defined type that holds data from the Transaction table in the database. 
public class Transaction
{
    public Transaction()
    {
        
    }

    [SetsRequiredMembers]
    public Transaction(char transactionType, int accountNumber, int? destinationAccountNumber, decimal amount,
        string comment, DateTime transactionTimeUtc)
    {
        TransactionType = transactionType;
        AccountNumber = accountNumber;
        DestinationAccountNumber = destinationAccountNumber ?? null;
        Amount = amount;
        Comment = comment ?? null;
        TransactionTimeUtc = transactionTimeUtc;
    }
    
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
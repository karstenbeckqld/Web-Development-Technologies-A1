using System.Diagnostics.CodeAnalysis;
using A1ClassLibrary.attributes;
using A1ClassLibrary.Interfaces;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary.model;

// The Withdraw class represents a user defined type that holds data from the Withdraw table in the database. 
public class Transaction : IModel
{
    public Transaction()
    {
    }

    [SetsRequiredMembers]
    public Transaction(string transactionType, int accountNumber, int? destinationAccountNumber, decimal amount,
        string comment, DateTime transactionTimeUtc)
    {
        TransactionType = transactionType;
        AccountNumber = accountNumber;
        DestinationAccountNumber = destinationAccountNumber;
        Amount = amount;
        Comment = comment;
        TransactionTimeUtc = transactionTimeUtc;
    }
    
    [SkipProperty] public int TransactionID { get; set; }
    public required string TransactionType { get; set; }
    public required int AccountNumber { get; set; }
    public int? DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }  
    public string Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }
    
    [SkipProperty] public int CustomerID { get; set; }

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
using System.Diagnostics.CodeAnalysis;
using MyBankDbAccess.attributes;


namespace MyBankDbAccess.Models;

// The Withdraw class represents a user defined type that holds data from the Withdraw table in the database. 
public class Transaction 
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
    
    [SkipProperty]  public int TransactionID { get; set; }
    public required string TransactionType { get; set; }
    public required int AccountNumber { get; set; }
    public int? DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }  
    public string Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }

    public override string ToString()
    {

        string output = "|";

        output += FixedLength(TransactionID.ToString(), 4, ' ');
        output += "|";

        switch (TransactionType)
        {
            case "D":
                output += FixedLength("Deposit", 14, ' ');
                break;
            case "W":
                output += FixedLength("Withdraw", 14, ' ');
                break;
            case "T":
                output += FixedLength("Transfer", 14, ' ');
                break;
            case "S":
                output += FixedLength("Service Charge", 14, ' ');
                break;
        }
        output += "|";

        output += FixedLength(Amount.ToString("c2"), 10, ' ');
        
        output += "|";

        output += FixedLength(DestinationAccountNumber.ToString(),20,' ') + "|";

        output += FixedLength(Comment, 32, ' ');
        
        output += "|";

        output += FixedLength(TransactionTimeUtc.ToString(),23,' ')+"|";

        return output;
    }
    
    //Reference for method "FixedLength"
    
    //Panoch, J. and wakawaka (2022) How to truncate or pad a string to a fixed length in C#,
    //Stack Overflow. Available at: https://stackoverflow.com/questions/43096857/how-to-truncate-or-pad-a-string-to-a-fixed-length-in-c-sharp
    //(Accessed: 28 June 2023). 
    public static string FixedLength(string value, int totalWidth, char paddingChar)
    {
        if (value is null)
            return new string(paddingChar, totalWidth);

        if (value.Length > totalWidth)
            return value.Substring(0, totalWidth);
        else
            return value.PadRight(totalWidth, paddingChar);
    }

  
}
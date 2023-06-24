using System.Diagnostics.CodeAnalysis;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary.model;

// The Account class represents a user defined type that holds data from the Account table in the database. 
public class Account
{
    public Account()
    {
    }

    [SetsRequiredMembers]
    public Account(int accountNumber, string accountType, int customerId, decimal balance)
    {
        AccountNumber = accountNumber;
        AccountType = accountType;
        CustomerID = customerId;
        Balance = balance;
    }

    [PrimaryKey]public int AccountNumber { get; set; }
    public string AccountType { get; set; }
    public required int CustomerID { get; set; }
    public decimal Balance { get; set; }
    [SkipProperty] public List<Transaction> Transactions { get; set; }

    public override string ToString()
    {
        return
            $"AccountNumber: {AccountNumber}, AccountType: {AccountType}, CustomerID: {CustomerID}, Balance: {Balance}";
    }
}
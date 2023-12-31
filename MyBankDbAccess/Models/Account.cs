﻿using System.Diagnostics.CodeAnalysis;
using MyBankDbAccess.attributes;

namespace MyBankDbAccess.Models;

// The Account class represents a user defined type that holds data from the Account table in the database. 
public class Account
{
    public static readonly decimal SavingMinBalance = 0;
    public static readonly decimal CreditMinBalance = 300;
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

    public int AccountNumber { get; set; }
    public string AccountType { get; set; }
    public required int CustomerID { get; set; }
    public decimal Balance { get; set; }
    [SkipProperty] public List<Transaction> Transactions { get; set; }

    public override string ToString()
    {
        return
            $"AccountNumber: {AccountNumber}, AccountType: {AccountType}, CustomerID: {CustomerID}, Balance: {Balance}";
    }

    public string GetAccountNiceName()
    {
        return AccountType.Equals("C") ? "Credit" : "Savings";
    }

    public decimal GetMinimumBalance()
    {
        return AccountType.Equals("C") ? Account.CreditMinBalance : Account.SavingMinBalance;
    }
}
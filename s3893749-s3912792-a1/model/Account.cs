﻿using System.Data.SqlTypes;

namespace s3893749_s3912792_a1.model
{
    public class Account
    {
        private List<Transaction> _transactions;

        public Account(int accountNumber, int customerId, SqlMoney accountBalance, char accountType)
        {
            AccountNumber = accountNumber;
            CustomerId = customerId;
            AccountBalance = accountBalance;
            AccountType = accountType;
            _transactions = new List<Transaction>();
        }

        public int AccountNumber { get; }
        public int CustomerId { get; }
        public SqlMoney AccountBalance { get; private set; }
        public char AccountType { get; set; }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }
    }
}
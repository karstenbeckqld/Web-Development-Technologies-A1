using System.Data.SqlTypes;

namespace s3893749_s3912792_a1.model
{
    public class Account
    {
        private List<Transaction> _transactions;

        public Account(int accountNumber, int customerId, decimal accountBalance, string accountType)
        {
            AccountNumber = accountNumber;
            CustomerID = customerId;
            AccountBalance = accountBalance;
            AccountType = accountType;
            _transactions = new List<Transaction>();
        }

        public int CustomerID { get; set; }
        public int AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public string AccountType { get; set; }

        public List<Transaction> Transactions { get; set; }

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
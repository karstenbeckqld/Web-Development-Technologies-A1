using System.Data.SqlTypes;
using System.Security.Principal;
using s3893749_s3912792_a1.interfaces;

namespace s3893749_s3912792_a1.model
{
    public class Account
    {

        /*private int _accountNumber;
        private string _accountType;
        private int _customerId;
        private decimal _balance;

        public Account(int accountNumber, string accountType, int customerId, decimal balance)
        {
            _accountNumber = accountNumber;
            _accountType = accountType;
            _customerId = customerId;
            _balance = balance;
        }*/
        
        public int AccountNumber { get; set; }
        public string AccountType { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
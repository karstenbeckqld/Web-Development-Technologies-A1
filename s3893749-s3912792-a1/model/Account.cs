using System.Data.SqlTypes;
using s3893749_s3912792_a1.interfaces;

namespace s3893749_s3912792_a1.model
{
    public class Account : IDatabaseObject
    {
        public int AccountNumber { get; set; }
        public string AccountType { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
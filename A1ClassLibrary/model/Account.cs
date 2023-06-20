
namespace A1ClassLibrary.model
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public string AccountType { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }

        public override string ToString()
        {
            return $"AccountNumber: {AccountNumber}, AccountType: {AccountType}, CustomerID: {CustomerId}, Balance: {Balance}";
        }
    }
}
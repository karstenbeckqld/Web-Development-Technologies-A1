using A1ClassLibrary.DBControllers;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary.model
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public char AccountType { get; set; }
         public required int CustomerId { get; set; }
        public decimal Balance { get; set; }
        [SkipProperty] public List<Transaction> Transactions { get; set; }

        public override string ToString()
        {
            return
                $"AccountNumber: {AccountNumber}, AccountType: {AccountType}, CustomerID: {CustomerId}, Balance: {Balance}";
        }
    }
}
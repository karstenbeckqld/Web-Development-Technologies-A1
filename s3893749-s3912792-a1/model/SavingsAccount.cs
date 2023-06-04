using System.Data.SqlTypes;

namespace s3893749_s3912792_a1.model
{
    internal class SavingsAccount : Account
    {
        public SavingsAccount(int index, char accountName, int accountNumber, int customerId, SqlMoney accountBalance)
            : base(index, accountNumber, customerId, accountBalance)
        {
            AccountName = accountName;
        }

        public char AccountName { get; set; }
    }
}

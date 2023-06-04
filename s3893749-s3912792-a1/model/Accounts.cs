using System.Data.SqlTypes;

namespace s3893749_s3912792_a1.model
{
    public class Accounts
    {
        private List<Account> bankAccounts;
        private int _numAccounts;

        public Accounts()
        {
            bankAccounts = new List<Account>();
            _numAccounts = 0;
        }

        internal List<Account>? BankAccounts { get; }

        public void AddSavingsAccount(char accountName, int accountNumber, int customerId, SqlMoney accountBalance)
        {
            bankAccounts.Add(new SavingsAccount(_numAccounts, accountName, accountNumber, customerId, accountBalance));
            _numAccounts++;
        }

        public void AddCheckAccount(char accountName, int customerId, int accountNumber, SqlMoney accountBalance)
        {
            bankAccounts.Add(new CheckAccount(_numAccounts, accountName, accountNumber, customerId, accountBalance));
            _numAccounts++;
        }



    }
}
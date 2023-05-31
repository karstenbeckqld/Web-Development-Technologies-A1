using System;
using System.Data.SqlTypes;

namespace s3893749_s3912792_a1
{
	public class Accounts
	{
		private List<Account> _accounts;

		private int _numAccounts;

		public Accounts()
		{
			_numAccounts = 0; 
		}

		public void addSavingsAccount(char accountName, int customerId, int accountNumber, SqlMoney accountBalance)
		{
			_accounts.Add(new SavingsAccount(accountName, accountNumber, customerId, accountBalance));
			_numAccounts++;
		}

        public void addCheckAccount(char accountName, int customerId, int accountNumber, SqlMoney accountBalance)
        {
            _accounts.Add(new CheckAccount(accountName, accountNumber, customerId, accountBalance));
            _numAccounts++;
        }
    }
}


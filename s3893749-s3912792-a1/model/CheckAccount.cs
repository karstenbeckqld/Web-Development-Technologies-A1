using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s3893749_s3912792_a1
{
    internal class CheckAccount : Account
    {

        private char _accountName;

        public CheckAccount(int index,char accountName, int accountNumber, int customerId, SqlMoney accountBalance)
            :base(index, accountNumber, customerId, accountBalance)
        {
            AccountName = accountName;
        }

        public char AccountName { get; set; }
    }
}

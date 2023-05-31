using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s3893749_s3912792_a1
{
    class Account
    {
        protected Account(int accountNumber, int customerId, SqlMoney accountBalance)
        {
            AccountNumber = accountNumber;
            CustomerId = customerId;
            AccountBalance = accountBalance;
        }

        public int AccountNumber { get; }
        public int CustomerId { get; }
        public SqlMoney AccountBalance { get; private set; }
    }
}
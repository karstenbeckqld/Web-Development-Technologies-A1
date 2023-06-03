using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s3893749_s3912792_a1.model
{
    class Account
    {
        private int _index;
        protected Account(int index, int accountNumber, int customerId, SqlMoney accountBalance)
        {
            AccountNumber = accountNumber;
            CustomerId = customerId;
            AccountBalance = accountBalance;
            Index = index;
        }

        protected int Index { get; }
        protected int AccountNumber { get; }
        protected int CustomerId { get; }
        protected SqlMoney AccountBalance { get; private set; }
    }
}
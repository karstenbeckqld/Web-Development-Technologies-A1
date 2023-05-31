using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s3893749_s3912792_a1
{
    public class Account
    {
        private int _AccountNumber;
        private int _CustomerId;
        private SqlMoney _AccountBalance;

        protected Account(int accountNumber, int customerId, SqlMoney accountBalance)
        {
            _AccountNumber = accountNumber;
            _CustomerId = customerId;
            _AccountBalance = accountBalance;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s3893749_s3912792_a1
{
    internal class SavingsAccount : Account
    {

        private char _AccountName;

        public SavingsAccount(char accountName, int accountNumber, int customerId, SqlMoney accountBalance) : base(
            accountNumber, customerId, accountBalance)
        {
            _AccountName = accountName;
        }
    }
}

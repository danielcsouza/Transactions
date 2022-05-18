﻿using Transactions.Enums;

namespace Transactions.Persistence.Models
{
    public class Account
    {
        public int Id { get; set; }
        public double Balance { get; private set; }

        public void setBalance(double value, OperationsEnum operation)
        {
            if (operation == OperationsEnum.Deposit)
                Balance += value;

            if (operation == OperationsEnum.withdraw)
                Balance -= value;
        }
    }    
}
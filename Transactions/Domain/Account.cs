using Transactions.Enums;

namespace Transactions.Domains
{
    public class Account
    {
        public string Id { get; set; }
        public double Balance { get; private set; }

        public void setBalance(double value, OperationsEnum operation)
        {
            if (operation == OperationsEnum.Deposit)
            {
                Balance += value;
            }
            
            if (operation == OperationsEnum.withdraw)
            {
                Balance -= value;
            }
    
        }

        public bool VerifyBalanceWithDraw(double value)
        {
            if (Balance < value)
                return false;

            return true;
        }
    }    
}

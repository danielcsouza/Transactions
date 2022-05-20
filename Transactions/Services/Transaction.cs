using Transactions.Domains;
using Transactions.Persistence.Repositories;
using Transactions.Services.ViewModels;

namespace Transactions.Services
{
    public class Transaction 
    {
        private ITransaction _itransaction;

        public Transaction()
        {
        }

        public Transaction(ITransaction transaction)
        {
            _itransaction = transaction;
        }
    
        public void DefineStrategy(ITransaction transaction)
        {
            _itransaction = transaction;
        }
    
        public ResultViewModel ExecuteTransaction(string? origin, string? destination, double value)
        {
           return _itransaction.ExecuteTransaction(origin, destination, value);
        }
    }
}

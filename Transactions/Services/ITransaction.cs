using Transactions.Domains;
using Transactions.Services.ViewModels;

namespace Transactions.Services
{
    public interface ITransaction
    {
        public ResultViewModel ExecuteTransaction(string? origin, string? destination, double value);
    }
}

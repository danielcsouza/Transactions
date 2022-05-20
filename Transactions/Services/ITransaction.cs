using Transactions.Domains;
using Transactions.Services.ViewModels;

namespace Transactions.Services
{
    public interface ITransaction
    {
        public ResultDepositViewModel Deposit(string? destination, double value);
        public ResultWithDrawViewModel WhitDraw(string origin, double value);
        public TransferDataViewModel Transfer(string origin, string destination, double value);
    }
}

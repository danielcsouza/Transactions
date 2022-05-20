namespace Transactions.Services.ViewModels
{
    public class ResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public double Balance { get; set; }
        public TransferDataViewModel TransactionResult { get; set; } = new TransferDataViewModel();

    }
}

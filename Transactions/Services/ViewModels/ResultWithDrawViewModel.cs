namespace Transactions.Services.ViewModels
{
    public class ResultWithDrawViewModel
    {
        public bool Success { get; set; }
        public string Messages { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public double Balance { get; set; }

    }
}

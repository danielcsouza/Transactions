namespace Transactions.Services.ViewModels
{
    public class ResultDepositViewModel
    {
        public bool Success { get; set; }
        public string Messages { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public double Balance { get; set; }

    }
}

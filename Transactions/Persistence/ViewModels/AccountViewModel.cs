namespace Transactions.Persistence.ViewModels
{
    public class AccountViewModel
    {
        public string Type { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public double Amount { get; set; }
        
    }
}

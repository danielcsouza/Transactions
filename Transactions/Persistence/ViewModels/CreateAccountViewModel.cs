namespace Transactions.Persistence.ViewModels
{
    public class CreateAccountViewModel
    {
        public string Type { get; set; }
        public string? Origin { get; set; }
        public string Destination { get; set; }
        public double Amount { get; set; }
        
    }
}

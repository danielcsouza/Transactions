using System.Text.Json.Serialization;

namespace Transactions.Persistence.ViewModels
{
    public class TransferDataViewModel
    {
        public TransferViewModelData Origin { get; set; }
        public TransferViewModelData Destination { get; set; }
        [JsonIgnore]
        public bool Operation { get; set; }
    }


    public class TransferViewModelData
    {
        public int Id { get; set; }
        public double Balance { get; set; }
    }
}

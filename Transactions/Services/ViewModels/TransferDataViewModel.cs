using System.Text.Json.Serialization;

namespace Transactions.Services.ViewModels
{
    public class TransferDataViewModel
    {
        public TransferViewModelData Origin { get; set; } = new TransferViewModelData();
        public TransferViewModelData Destination { get; set; } = new TransferViewModelData();

        [JsonIgnore]
        public bool Operation { get; set; }

        [JsonIgnore]
        public string Message { get; set; } = string.Empty;
    }


    public class TransferViewModelData
    {
        public string Id { get; set; } = string.Empty;
        public double Balance { get; set; }
    }
}

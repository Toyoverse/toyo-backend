namespace BackendToyo.Models
{
    public class SmartContractToyoSync
    {
        public string ChainId { get; set; }
        public string ContractAddress { get; set; }
        public string EventName { get; set; } 
        public int LastBlockNumber { get; set; } 
    }
}
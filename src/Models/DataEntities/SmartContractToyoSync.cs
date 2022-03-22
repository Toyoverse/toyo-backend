namespace BackendToyo.Models.DataEntities
{
    public class SmartContractToyoSync
    {
        public string ChainId { get; set; }
        public string ContractAddress { get; set; }
        public string EventName { get; set; } 
        public int LastBlockNumber { get; set; } 
    }
}
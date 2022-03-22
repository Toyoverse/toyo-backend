namespace BackendToyo.Models.DataEntities
{
    public class SmartContractToyoTransfer
    {
        public string TransactionHash { get; set; } 
        public int TokenId { get; set; } 
        public string ChainId { get; set; }
        public int BlockNumber { get; set; }         
        public string WalletAddress { get; set; }
    }
}
namespace BackendToyo.Models.DataEntities
{
    public class SmartContractToyoType
    {
        public string TransactionHash { get; set; } 
        public int TypeId { get; set; } 
        public string ChainId { get; set; }
        public int BlockNumber { get; set; }         
        public string Sender { get; set; }
        public string Name { get; set; }
        public string MetaDataUrl { get; set; }
    }
}
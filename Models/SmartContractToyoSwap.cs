namespace BackendToyo.Models
{
    public class SmartContractToyoSwap
    {
        public string TransactionHash { get; set; }
        public int TokenId { get; set; }
        public string ChainId { get; set; }
        public int FromTypeId { get; set; }
        public int ToTypeId { get; set; }
        public string Sender { get; set; }
        public int BlockNumber { get; set; }
    }
}
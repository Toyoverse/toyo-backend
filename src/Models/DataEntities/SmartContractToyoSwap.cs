namespace BackendToyo.Models.DataEntities
{ //sinx - 10 caixas fortified - typeid2
    public class SmartContractToyoSwap
    {
        public string TransactionHash { get; set; }
        public int FromTokenId { get; set; }
        public int ToTokenId { get; set; }
        public string ChainId { get; set; }
        public int FromTypeId { get; set; }
        public int ToTypeId { get; set; }
        public string Sender { get; set; }
        public int BlockNumber { get; set; }
    }
}


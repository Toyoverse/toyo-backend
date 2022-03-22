using System;

namespace BackendToyo.Models.DataEntities
{
    public class SmartContractToyoMint
    {
        public string TransactionHash { get; set; } 
        public int TokenId { get; set; } 
        public string ChainId { get; set; }
        public string Sender { get; set; }
        public string WalletAddress { get; set; } 
        public int TypeId { get; set; } 
        public int TotalSypply { get; set; } 
        public Int64 Gwei { get; set; } 
        public int BlockNumber { get; set; } 
    }
}
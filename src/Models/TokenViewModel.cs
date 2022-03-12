using System;

namespace BackendToyo.Models
{
    public class TokenViewModel
    {
        public Int64 TokenId { get; set; }
        public string WalletAddress { get; set; }
        public Int64 TypeId { get; set; }
        public Int64 BlockNumber { get; set; }
        public string TransactionHash { get; set; } 
        public int ChainId { get; set; }
    }
}
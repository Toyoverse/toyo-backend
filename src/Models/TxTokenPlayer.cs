using System;

namespace BackendToyo.Models
{
    public class TxTokenPlayer
    {
        public Guid Id { get; set; }
        public string TxHash { get; set; }
        public Int64 BlockNumber { get; set; }
        public string TxTimeStamp { get; set; }
        public Guid PlayerId { get; set; }
        public Guid TokenId { get; set; }
        public Int64 ChainId { get; set; }
        public string ToyoSku { get; set; }
    }
}
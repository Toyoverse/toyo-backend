using System;

namespace BackendToyo.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string User { get; set; }
        public string JoinTimeStamp { get; set; }  
        public string Mail { get; set; }
        public string Name { get; set; }
        public string WalletAddress { get; set; }
    }
}
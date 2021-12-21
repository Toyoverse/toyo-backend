namespace BackendToyo.Models
{
    public class PartPlayer
    {
        public int Id { get; set; }  
        public int PartId { get; set; }
        public int StatId { get; set; }
        public int BonusStat { get; set; }
        public int TokenId { get; set; }
        public string WalletAddress { get; set; }
        public string ChainId { get; set; }
    }
}
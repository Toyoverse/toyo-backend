namespace BackendToyo.Models
{
    public class ToyoPlayer
    {
        public int Id { get; set; } 
        public int ToyoId { get; set; }
        public int TokenId { get; set; }
        public int Vitality { get; set; }
        public int Strength { get; set; }
        public int Resistance { get; set; }
        public int CyberForce { get; set; }
        public int Resilience { get; set; }
        public int Precision { get; set; }
        public int Technique { get; set; }
        public int Analysis { get; set; }
        public int Speed { get; set; }
        public int Agility { get; set; }
        public int Stamina { get; set; }
        public int Luck { get; set; }
        public string WalletAddress { get; set; }
        public string ChainId { get; set; }
        public bool ChangeValue { get; set; }
    }
}
namespace BackendToyo.Models
{
    public class Items
    {
        public int Id { get; set; } 
        public int Nome  { get; set; } 
        public int Attacheable  { get; set; } 
        public int Usage  { get; set; } 
        public int Cyber  { get; set; } 
        public int Stat  { get; set; } 
        public int Max  { get; set; } 
        public int Durability  { get; set; } 
        public string Sprite  { get; set; } 
        public string Stackable  { get; set; }
        public string Price  { get; set; }
        public string Nft  { get; set; }
        public string Descricao  { get; set; }
        public string Soulbound  { get; set; }
    }
}
namespace BackendToyo.Models
{
    public class Toyo
    {
        public int Id { get; set; } 
        public int Existe  { get; set; } 
        public int Material  { get; set; } 
        public int BodyType  { get; set; } 
        public int Rarity  { get; set; } 
        public int Size  { get; set; } 
        public int Variants  { get; set; } 
        public int Colors  { get; set; } 
        public int Cyber  { get; set; } 
        public string Name  { get; set; } 
        public string Desc  { get; set; } 
        public string Thumb { get; set; }
        public string Video { get; set; }
        public string Region { get; set; }
    }

    public class SwapToyo
    {
        public string TransactionHash { get; set; }
        public string ChainId { get; set; }
        public int ToTokenId { get; set; }
        public int TypeToken { get; set; }
        public string Name { get; set; }
    }
}
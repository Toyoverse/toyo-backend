namespace BackendToyo.Models.DataEntities
{
    public class Parts
    {
        public int Id { get; set; } 
        public int TorsoId  { get; set; } 
        public int Part  { get; set; } 
        public int RetroBone  { get; set; } 
        public int Size  { get; set; } 
        public int Variants  { get; set; } 
        public int Colors  { get; set; } 
        public int Cyber  { get; set; } 
        public string Prefix  { get; set; } 
        public string Desc  { get; set; }
    }
}
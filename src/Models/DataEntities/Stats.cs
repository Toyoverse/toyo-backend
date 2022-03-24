namespace BackendToyo.Models.DataEntities
{
    public class Stats
    {
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
    }

     public class Stat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
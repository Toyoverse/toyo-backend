namespace BackendToyo.Models
{
    public class PartsStatsViewModel
    {
        public PartsStatsViewModel(int _partId, string _statName, int _bonusStat) {
            PartId = _partId;
            StatName = _statName;
            BonusStat = _bonusStat;
        }

        public int PartId  { get; set; }
        public string StatName  { get; set; }
        public int BonusStat  { get; set; }
    }

    public class ToyoStatsViewModel
    {
        public ToyoStatsViewModel(int _toyoId, int _vitality, int _strength, int _resistance, int _cyberForce, int _resilience, int _precision, int _technique, int _analysis, int _speed, int _agility, int _stamina, int _luck) {
            ToyoId = _toyoId;
            Vitality = _vitality;
            Strength = _strength;
            Resistance = _resistance;
            CyberForce = _cyberForce;
            Resilience = _resilience;
            Precision = _precision;
            Technique = _technique;
            Analysis = _analysis;
            Speed = _speed;
            Agility = _agility;
            Stamina = _stamina;
            Luck = _luck;
        }

        public int ToyoId { get; set; }
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
}
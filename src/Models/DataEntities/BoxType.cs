namespace BackendToyo.Models.DataEntities
{
    public class BoxType
    {
        public int BoxTypeId {get;set;}
        public int TypeId {get;set;}
        public bool IsJakana {get;set;}
        public bool IsFortified {get; set;}
        public TypeToken TypeToken { get; set; }
    }
}
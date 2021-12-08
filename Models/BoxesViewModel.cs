namespace BackendToyo.Models
{
    public class BoxesViewModel
    {
        public BoxesViewModel(int _tokenId, int _typeId, string _name) {
            TokenId = _tokenId;
            TypeId = _typeId;
            Name = _name;
        }

        public int TokenId  { get; set; }
        public int TypeId  { get; set; }
        public string Name  { get; set; }
    }
}
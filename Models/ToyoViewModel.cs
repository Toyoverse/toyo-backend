namespace BackendToyo.Models
{
    public class ToyoViewModel
    {
        public ToyoViewModel(int _tokenId, string _name, string _thumb, string _animation, bool _changeValue) {
            TokenId = _tokenId;
            Name = _name;
            Thumb = _thumb;
            Animation = _animation;
            ChangeValue = _changeValue;
        }

        public int TokenId  { get; set; }
        public string Name  { get; set; }
        public string Thumb  { get; set; }
        public string Animation  { get; set; }
        public bool ChangeValue  { get; set; }
    }
}
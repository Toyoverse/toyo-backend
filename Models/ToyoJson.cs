namespace BackendToyo.Models
{
    public class ToyoJson
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string AnimationUrl { get; set; }
        public AttributesJson[] attributes { get; set; }
    }
}
using Dietician.Enums;

namespace Dietician.Models
{
    public class FatLevel
    {
        public Lifestyle LifeStyle { get; set; }
        public int WaistSize { get; set; }
        public int HipSize { get; set; }
        public int TricepsFold { get; set; }
        public int HipFold { get; set; }
        public int ThighFold { get; set; }
        public int ChestFold { get; set; }
        public int BellyFold { get; set; }
    }
}
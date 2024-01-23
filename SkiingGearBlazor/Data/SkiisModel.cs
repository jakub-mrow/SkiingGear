using Core;
using Interfaces;

namespace SkiingGearBlazor.Data
{
    public class SkiisModel : ISkiis
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public ISkiBrand Brand { get; set; }
        public SkiType Type { get; set; }
        public int Length { get; set; }
        public int Price { get; set; }
    }
}

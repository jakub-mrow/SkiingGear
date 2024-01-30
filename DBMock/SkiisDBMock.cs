using MularczykMrowczynski.SkiingGear.Core;
using MularczykMrowczynski.SkiingGear.Interfaces;

namespace MularczykMrowczynski.SkiingGear.DBMock
{
    public class SkiisDBMock: ISkiis
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public ISkiBrand Brand { get; set; }
        public SkiType Type { get; set; }
        public int Length { get; set; }
        public int Price { get; set; }
    }
}

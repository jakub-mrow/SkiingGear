using MularczykMrowczynski.SkiingGear.Interfaces;

namespace MularczykMrowczynski.SkiingGear.DBMock
{
    public class SkiBrandDBMock: ISkiBrand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int FoundationYear { get; set; }
    }
}

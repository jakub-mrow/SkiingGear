using Interfaces;
using System.ComponentModel.DataAnnotations;


namespace SkiingGear.DBSQL
{
    public class SkiBrandDBSQL
    {
        [Key]
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int FoundationYear { get; set; }

        public ISkiBrand ToISkiBrand()
        {
            return new SkiBrand() { BrandId = BrandId, Name = Name, Country = Country, FoundationYear = FoundationYear };
        }
    }

    public class SkiBrand : ISkiBrand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int FoundationYear { get; set; }
    }
}

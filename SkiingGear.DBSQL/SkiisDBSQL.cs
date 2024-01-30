using System.ComponentModel.DataAnnotations;
using MularczykMrowczynski.SkiingGear.Interfaces;
using MularczykMrowczynski.SkiingGear.Core;

namespace MularczykMrowczynski.SkiingGear.SkiingGear.DBSQL
{
    public class SkiisDBSQL
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public int BrandId { get; set; }
        public SkiType Type { get; set; }
        public int Length { get; set; }
        public int Price { get; set; }


        public ISkiis ToISkiis(List<SkiBrandDBSQL> skiBrands)
        {
            return new Skiis() { Id = Id, Model = Model, Brand = skiBrands.Single(skibrand => skibrand.BrandId.Equals(BrandId)).ToISkiBrand(), Type = Type, Length = Length, Price = Price};  
        }
    }

    public class Skiis : ISkiis
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public ISkiBrand Brand { get; set; }
        public SkiType Type { get; set; }
        public int Length { get; set; }
        public int Price { get; set; }

    }
}

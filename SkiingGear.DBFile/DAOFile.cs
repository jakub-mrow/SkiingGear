using Core;
using Interfaces;

namespace SkiingGear.DBFile
{
    public class DAOFile
    {
        private List<ISkiis> skiis;
        private List<ISkiBrand> skiBrands;

        public DAOFile() 
        { 
            skiis = new List<ISkiis>();
            skiBrands = new List<ISkiBrand>();


            AddNewSkiBrand(new SkiBrandDBFile() { BrandId = 1, Country = "Germany", FoundationYear = 1960, Name = "Volkl" });
            AddNewSkiBrand(new SkiBrandDBFile() { BrandId = 2, Country = "Austria", FoundationYear = 1924, Name = "Atomic" });
            AddNewSkiBrand(new SkiBrandDBFile() { BrandId = 3, Country = "Switzerland", FoundationYear = 1947, Name = "Head" });


            AddNewSkiis(new SkiisDBFile() { Id = 1, Length = 160, Price = 1100, Type = SkiType.AllMountain, Brand = skiBrands[0], Model = "RaceTiger" });

            AddNewSkiis(new SkiisDBFile()
            {
                Id = 2,
                Length = 165,
                Price = 1200,
                Type = SkiType.Slalom,
                Brand = skiBrands[1],
                Model = "Speedster"
            });

            AddNewSkiis(new SkiisDBFile()
            {
                Id = 3,
                Length = 170,
                Price = 1300,
                Type = SkiType.Freestyle,
                Brand = skiBrands[2],
                Model = "Airborne"
            });

            AddNewSkiis(new SkiisDBFile()
            {
                Id = 4,
                Length = 175,
                Price = 1400,
                Type = SkiType.AllMountain,
                Brand = skiBrands[0],
                Model = "Explorer"
            });

            AddNewSkiis(new SkiisDBFile()
            {
                Id = 5,
                Length = 160,
                Price = 1200,
                Type = SkiType.Slalom,
                Brand = skiBrands[1],
                Model = "Rocket"
            });

            AddNewSkiis(new SkiisDBFile()
            {
                Id = 6,
                Length = 165,
                Price = 1100,
                Type = SkiType.Freestyle,
                Brand = skiBrands[2],
                Model = "Gravity"
            });
        }


        private void SaveSkiis()
        {
            Serializer.Serialize("skiis.bin", skiis);
        }

        private void SaveSkiBrands()
        {
            Serializer.Serialize("skibrands.bin", skiBrands);
        }

        public IEnumerable<ISkiBrand> GetAllSkiBrands()
        {
            return skiBrands;
        }
        public IEnumerable<ISkiis> GetAllSkiis()
        {
            return skiis;
        }

        public ISkiBrand AddNewSkiBrand(ISkiBrand skiBrand)
        {
            skiBrands.Add(skiBrand);
            SaveSkiBrands();
            return skiBrand;
        }
        public ISkiis AddNewSkiis(ISkiis newSkiis)
        {
            skiis.Add(newSkiis);
            SaveSkiis();
            return newSkiis;
        }

        public void RemoveSkiBrand(int skiBrandId)
        {
            ISkiBrand skiBrandToRemove = skiBrands.First(brand => brand.BrandId.Equals(skiBrandId));
            skiBrands.Remove(skiBrandToRemove);
            SaveSkiBrands();
        }
        public void RemoveSkiis(int skiisId)
        {
            ISkiis skiisToRemove = skiis.First(skiis => skiis.Id.Equals(skiisId));
            skiis.Remove(skiisToRemove);
            SaveSkiis();
        }

        public void UpdateSkiBrand(ISkiBrand skiBrand)
        {
            int skiBrandIndex = skiBrands.FindIndex(brand => brand.BrandId.Equals(skiBrand.BrandId));
            skiBrands[skiBrandIndex] = skiBrand;
            SaveSkiBrands();
        }
        public void UpdateSkiis(ISkiis updatedSkiis)
        {
            int skiisIndex = skiis.FindIndex(s => s.Id.Equals(updatedSkiis.Id));
            skiis[skiisIndex] = updatedSkiis;
            SaveSkiis();
        }
    }
}

using Core;
using Interfaces;

namespace DBMock
{
    public class DAOMock: IDAO
    {
        private List<ISkiis> skiis;
        private List<ISkiBrand> skiBrands;

        public DAOMock()
        {
            skiis = new List<ISkiis>();
            skiBrands = new List<ISkiBrand>();

            AddNewSkiBrand(new SkiBrandDBMock() { Country = "Germany", FoundationYear = 1960, Name = "Volkl" });
            AddNewSkiBrand(new SkiBrandDBMock() { Country = "Austria", FoundationYear = 1924, Name = "Atomic" });
            AddNewSkiBrand(new SkiBrandDBMock() { Country = "Switzerland", FoundationYear = 1947, Name = "Head" });


            AddNewSkiis(new SkiisDBMock() { Length = 160, Price = 1100, Type = SkiType.AllMountain, Brand = skiBrands[0], Model = "RaceTiger" });

            AddNewSkiis(new SkiisDBMock()
            {
                Length = 165,
                Price = 1200,
                Type = SkiType.Slalom,
                Brand = skiBrands[1],
                Model = "Speedster"
            });

            AddNewSkiis(new SkiisDBMock()
            {
                Length = 170,
                Price = 1300,
                Type = SkiType.Freestyle,
                Brand = skiBrands[2],
                Model = "Airborne"
            });

            AddNewSkiis(new SkiisDBMock()
            {
                Length = 175,
                Price = 1400,
                Type = SkiType.AllMountain,
                Brand = skiBrands[0],
                Model = "Explorer"
            });

            AddNewSkiis(new SkiisDBMock()
            {
                Length = 160,
                Price = 1200,
                Type = SkiType.Slalom,
                Brand = skiBrands[1],
                Model = "Rocket"
            });

            AddNewSkiis(new SkiisDBMock()
            {
                Length = 165,
                Price = 1100,
                Type = SkiType.Freestyle,
                Brand = skiBrands[2],
                Model = "Gravity"
            });
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
            skiBrand.BrandId = skiBrands.Count + 1;
            skiBrands.Add(skiBrand);
            return skiBrand;
        }
        public ISkiis AddNewSkiis(ISkiis newSkiis)
        {
            newSkiis.Id = skiis.Count + 1;
            skiis.Add(newSkiis);
            return newSkiis;
        }

        public void RemoveSkiBrand(int skiBrandId)
        {
            //List<ISkiis> skiisToRemove = skiis.Where(s => s.Brand.BrandId.Equals(skiBrandId)).ToList();
            //foreach (ISkiis ski in skiisToRemove)
            //{
            //    skiis.Remove(ski);
            //}
            ISkiBrand skiBrandToRemove = skiBrands.First(brand => brand.BrandId.Equals(skiBrandId));
            skiBrands.Remove(skiBrandToRemove);
        }
        public void RemoveSkiis(int skiisId)
        {
            ISkiis skiisToRemove = skiis.First(skiis => skiis.Id.Equals(skiisId));
            skiis.Remove(skiisToRemove);
        }

        public void UpdateSkiBrand(ISkiBrand skiBrand)
        {
            int skiBrandIndex = skiBrands.FindIndex(brand => brand.BrandId.Equals(skiBrand.BrandId));
            skiBrands[skiBrandIndex] = skiBrand;
        }
        public void UpdateSkiis(ISkiis updatedSkiis)
        {
            int skiisIndex = skiis.FindIndex(s => s.Id.Equals(updatedSkiis.Id));
            skiis[skiisIndex] = updatedSkiis;
        }


    }
}

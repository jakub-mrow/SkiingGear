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

            AddNewSkiBrand(new SkiBrandDBMock() { BrandId = 1, Country = "Germany", FoundationYear = 1960, Name = "Volkl" });
            AddNewSkiBrand(new SkiBrandDBMock() { BrandId = 2, Country = "Austria", FoundationYear = 1924, Name = "Atomic" });
            AddNewSkiBrand(new SkiBrandDBMock() { BrandId = 3, Country = "Switzerland", FoundationYear = 1947, Name = "Head" });


            AddNewSkiis(new SkiisDBMock() { });
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
            return skiBrand;
        }
        public ISkiis AddNewSkiis(ISkiis newSkiis)
        {
            skiis.Add(newSkiis);
            return newSkiis;
        }

        public void RemoveSkiBrand(int skiBrandId)
        {
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

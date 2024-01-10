using Interfaces;

namespace SkiingGear.DBSQL
{
    public partial class DAOSQL
    {
        public IEnumerable<ISkiBrand> GetAllSkiBrands()
        {
            return SkiBrands.Select(skiis => skiis.ToISkiBrand());
        }

        public ISkiBrand AddNewSkiBrand(ISkiBrand newSkiBrand)
        {
            var addedSkiBrand = new SkiBrandDBSQL()
            {
                Name = newSkiBrand.Name,
                Country = newSkiBrand.Country,
                FoundationYear = newSkiBrand.FoundationYear,
            };

            Add(addedSkiBrand);

            //SaveChanges();
            newSkiBrand.BrandId = addedSkiBrand.BrandId;
            return newSkiBrand;
        }

        public void RemoveSkiBrand(int skiBrandId)
        {
            var skiBrand = SkiBrands.FirstOrDefault(skiBrand => skiBrand.BrandId.Equals(skiBrandId));
            Remove(skiBrand);
            SaveChanges();
        }

        public void UpdateSkiBrand(ISkiBrand newUpdatedSkiBrand)
        {
            var skiBrand = SkiBrands.FirstOrDefault(skiBrand => skiBrand.BrandId.Equals(newUpdatedSkiBrand.BrandId));
            skiBrand.FoundationYear = newUpdatedSkiBrand.FoundationYear;
            skiBrand.Country = newUpdatedSkiBrand.Country;
            skiBrand.Name = newUpdatedSkiBrand.Name;

            Entry(skiBrand).CurrentValues.SetValues(skiBrand);
        }
    }
}

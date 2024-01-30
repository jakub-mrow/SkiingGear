using MularczykMrowczynski.SkiingGear.Interfaces;

namespace MularczykMrowczynski.SkiingGear.SkiingGear.DBSQL
{
    public partial class DAOSQL
    {
        public IEnumerable<ISkiBrand> GetAllSkiBrands()
        {
            return _context.SkiBrands.Select(skiis => skiis.ToISkiBrand());
        }

        public ISkiBrand AddNewSkiBrand(ISkiBrand newSkiBrand)
        {
            var addedSkiBrand = new SkiBrandDBSQL()
            {
                Name = newSkiBrand.Name,
                Country = newSkiBrand.Country,
                FoundationYear = newSkiBrand.FoundationYear,
            };

            _context.Add(addedSkiBrand);

            _context.SaveChanges();
            newSkiBrand.BrandId = addedSkiBrand.BrandId;
            return newSkiBrand;
        }

        public void RemoveSkiBrand(int skiBrandId)
        {
            var skiBrand = _context.SkiBrands.FirstOrDefault(skiBrand => skiBrand.BrandId.Equals(skiBrandId));
            _context.Remove(skiBrand);
            _context.SaveChanges();
        }

        public void UpdateSkiBrand(ISkiBrand newUpdatedSkiBrand)
        {
            var skiBrand = _context.SkiBrands.FirstOrDefault(skiBrand => skiBrand.BrandId.Equals(newUpdatedSkiBrand.BrandId));
            skiBrand.FoundationYear = newUpdatedSkiBrand.FoundationYear;
            skiBrand.Country = newUpdatedSkiBrand.Country;
            skiBrand.Name = newUpdatedSkiBrand.Name;

            _context.Entry(skiBrand).CurrentValues.SetValues(skiBrand);
        }
    }
}

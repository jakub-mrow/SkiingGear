using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiingGear.DBSQL
{
    public partial class DAOSQL
    {
        public IEnumerable<ISkiis> GetAllSkiis()
        {
            return Skiis.Select(skiis => skiis.ToISkiis(SkiBrands.ToList()));
        }

        public ISkiis AddNewSkiis(ISkiis newSkiis)
        {
            var addedNewSkiis = new SkiisDBSQL()
            {
                Model = newSkiis.Model,
                Type = newSkiis.Type,
                Length = newSkiis.Length,
                Price = newSkiis.Price,
                BrandId = newSkiis.Brand.BrandId
            };

            Add(addedNewSkiis);
            SaveChanges();
            newSkiis.Id = addedNewSkiis.Id;
            return newSkiis;
        }

        public void RemoveSkiis(int SkiisId)
        {
            var skiis = Skiis.FirstOrDefault(skiis => skiis.Id == SkiisId);
            Remove(skiis);
            SaveChanges();

        }

        public void UpdateSkiis(ISkiis newUpdatedSkiis)
        {
            var skiis = Skiis.FirstOrDefault(skiis => skiis.Id == newUpdatedSkiis.Id);
            skiis.Model = newUpdatedSkiis.Model;
            skiis.Length = newUpdatedSkiis.Length;
            skiis.Type = newUpdatedSkiis.Type;
            skiis.BrandId = newUpdatedSkiis.Brand.BrandId;
            skiis.Price = newUpdatedSkiis.Price;

            Entry(skiis).CurrentValues.SetValues(skiis);

        }
    }
}

using Core;
using Interfaces;
using SkiingGear.DBSQL;

namespace BL
{
    public class BL
    {
        private IDAO dao;

        public BL()
        {
            dao = new DAOSQL();
        }

        public IEnumerable<ISkiBrand> GetAllSkiBrands() => dao.GetAllSkiBrands();
        public IEnumerable<ISkiis> GetAllSkiis() => dao.GetAllSkiis();

        public IEnumerable<ISkiBrand> GetSkiBrand(int BrandId) => dao.GetAllSkiBrands().Where(skiBrand => skiBrand.BrandId.Equals(BrandId));
        public IEnumerable<ISkiis> GetSkiis(int SkiiId) => dao.GetAllSkiis().Where(skiis => skiis.Id.Equals(SkiiId));

        public void RemoveSkiBrand(int BrandId) => dao.RemoveSkiBrand(BrandId);
        public void RemoveSkiis(int SkiiId) => dao.RemoveSkiis(SkiiId);

        public void UpdateSkiBrand(ISkiBrand skiBrand) => dao.UpdateSkiBrand(skiBrand);
        public void UpdateSkiis(ISkiis skiis) => dao.UpdateSkiis(skiis);

        public void AddSkiBrand(ISkiBrand skiBrand) => dao.AddNewSkiBrand(skiBrand);
        public void AddSkiis(ISkiis skiis) => dao.AddNewSkiis(skiis);

        // filtering
        public IEnumerable<ISkiis> FilterSkiisByType(SkiType skiType) => dao.GetAllSkiis().Where(skiis => skiis.Type.Equals(skiType));

        public IEnumerable<ISkiis> FilterSkiisByBrandName(string brandName) => dao.GetAllSkiis().Where(skiis => skiis.Brand.Name.Equals(brandName));

        // searching
        public IEnumerable<ISkiis> FindSkiisByModel(string model) => dao.GetAllSkiis().Where(skiis => skiis.Model.Contains(model));
        public IEnumerable<ISkiBrand> FindSkiBrandByName(string name) => dao.GetAllSkiBrands().Where(skiBrand => skiBrand.Name.Contains(name));
    }
}

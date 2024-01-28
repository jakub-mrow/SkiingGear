using BL;
using Core;
using Interfaces;


namespace SkiingGearBlazor.Data
{
    public class SkiingGearBLService
    {
        private BL.BL businessLayer;

        public SkiingGearBLService(BL.BL bl)
        {
            businessLayer = bl;
        }

        public IEnumerable<ISkiBrand> GetSkiBrands()
        {
            return businessLayer.GetAllSkiBrands();
        }

        public void AddNewSkiBrand(ISkiBrand newSkiBrand)
        {
            businessLayer.AddSkiBrand(newSkiBrand);
        }

        public void RemoveSkiBrand(ISkiBrand skiBrand)
        {
            businessLayer.RemoveSkiBrand(skiBrand.BrandId);
        }

        public void UpdateSkiBrand(ISkiBrand updatedSkiBrand)
        {
            businessLayer.UpdateSkiBrand(updatedSkiBrand);
        }

        public IEnumerable<ISkiBrand> FindSkiBrandByName(string name)
        {
            return businessLayer.FindSkiBrandByName(name);
        }


        public IEnumerable<ISkiis> GetAllSkiis()
        {
            return businessLayer.GetAllSkiis();
        }

        public void AddNewSkiis(ISkiis newSkiis)
        {
            businessLayer.AddSkiis(newSkiis);
        }

        public void DeleteSkiis(int skiisId)
        {
            businessLayer.RemoveSkiis(skiisId);
        }

        public void UpdateSkiis(ISkiis newSkiis)
        {
            businessLayer.UpdateSkiis(newSkiis);
        }

        public IEnumerable<ISkiis> FindSkiisByModel(string name)
        {
            return businessLayer.FindSkiisByModel(name);
        }

        public IEnumerable<ISkiis> FilterSkiisBySkiType(SkiType skiType)
        {
            return businessLayer.FilterSkiisByType(skiType);
        }
    }
}

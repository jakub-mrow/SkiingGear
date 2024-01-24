using Core;
using Interfaces;
using System.Reflection;

namespace BL
{
    public class BL
    {
        private IDAO dao;
        private static BL instance;
        private static readonly object lockObject = new object();

        public BL(string filePath)
        {
            if (filePath.EndsWith(".dll"))
                LoadLibrary(filePath);
        }

        public static BL GetInstance(string filePath)
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new BL(filePath);
                    }
                } 
            }

            return instance;
        }

        public void LoadLibrary(string dllPath)
        {
            var typeToCreate = FindDAOType(dllPath);

            if (typeToCreate != null)
            {
                dao = CreateDAOInstance(typeToCreate);
            }
            else
            {
                throw new InvalidOperationException("No compatible IDAO type found in assembly.");
            }
        }

        private Type FindDAOType(string dllPath)
        {
            try
            {
                var assembly = Assembly.UnsafeLoadFrom(dllPath);
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(IDAO).IsAssignableFrom(type))
                    {
                        return type;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load assembly or find IDAO: " + ex.Message);
                throw;
            }

            return null;
        }

        private IDAO CreateDAOInstance(Type daoType)
        {
            try
            {
                return (IDAO)Activator.CreateInstance(daoType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create instance of IDAO: {daoType.Name}\n{ex.Message}");
                throw;
            }
        }

        public IEnumerable<ISkiBrand> GetAllSkiBrands() => dao.GetAllSkiBrands();
        public IEnumerable<ISkiis> GetAllSkiis() => dao.GetAllSkiis();

        public IEnumerable<ISkiBrand> GetSkiBrand(int BrandId) => dao.GetAllSkiBrands().Where(skiBrand => skiBrand.BrandId.Equals(BrandId));
        public IEnumerable<ISkiis> GetSkiis(int SkiiId) => dao.GetAllSkiis().Where(skiis => skiis.Id.Equals(SkiiId));

        public void RemoveSkiBrand(int BrandId)
        {

            var skiisToDelete = dao.GetAllSkiis().Where(skiis => skiis.Brand.BrandId.Equals(BrandId)).ToList();
            //IEnumerable<ISkiis> skiisToDelete = dao.GetAllSkiis().Where(skiis => skiis.Brand.BrandId.Equals(BrandId));
            foreach(ISkiis ski in skiisToDelete)
            {
                dao.RemoveSkiis(ski.Id);
            }
            
            dao.RemoveSkiBrand(BrandId);
        } 
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

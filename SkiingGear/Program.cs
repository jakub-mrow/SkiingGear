using Interfaces;
using Core;
using SkiingGear.DBSQL;

namespace SkiingGear
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new DAOSQL();
            ISkiBrand skiBrand = new SkiBrand() { Country = "TestCountry", FoundationYear = 2003, Name = "Name Test" };
            ISkiBrand addedSkiBrand = db.AddNewSkiBrand(skiBrand);

            Console.WriteLine($"{addedSkiBrand.BrandId}");

            IEnumerable<ISkiBrand> skiBrands = db.GetAllSkiBrands();
            foreach (var brand in skiBrands)
            {
                Console.WriteLine($"BrandId: {brand.BrandId}, Country: {brand.Country}, FoundationYear: {brand.FoundationYear}, Name: {brand.Name}");
            }

            ISkiis skiis = new Skiis() { Length = 190, Price = 1100, Type = SkiType.Freestyle, Brand = addedSkiBrand, Model = "RaceTiger" };
            db.AddNewSkiis(skiis);
            IEnumerable<ISkiis> allSkiis = db.GetAllSkiis();
            foreach (var ski in allSkiis)
            {
                Console.WriteLine($"Model: {ski.Model}, Type: {ski.Type}, Length: {ski.Length}, Price: {ski.Price}, Brand: {ski.Brand.Name}");
            }

        }
    }
}


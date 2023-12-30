using Microsoft.EntityFrameworkCore;
using Interfaces;

namespace SkiingGear.DBSQL
{
    public partial class DAOSQL : DbContext, IDAO
    {
        public DbSet<SkiisDBSQL> Skiis { get; set; }
        public DbSet<SkiBrandDBSQL> SkiBrands { get; set; }
        public string DbPath { get; }

        public DAOSQL()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "skiinggear.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"data source={DbPath}");
        }
    }
}

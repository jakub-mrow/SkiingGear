using Microsoft.EntityFrameworkCore;
using MularczykMrowczynski.SkiingGear.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace MularczykMrowczynski.SkiingGear.SkiingGear.DBSQL
{
    public partial class DAOSQL : DbContext, IDAO
    {
        public string DbPath { get; }
        private readonly DatabaseContext _context;

        public DAOSQL()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "skiinggear.db");

            _context = CreateDbContext(new string[] { });
            _context.Database.OpenConnection();
        }

        ~DAOSQL()
        {
            _context.Database.CloseConnection();
        }

        private DatabaseContext? CreateDbContext(string[] strings)
        {
            var builder = new ConfigurationBuilder();
            var configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=" + DbPath);
            return new DatabaseContext(optionsBuilder.Options);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlite($"data source={DbPath}");
        //}
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiingGear.DBSQL
{
    public class DatabaseContext: DbContext
    {
        public DbSet<SkiisDBSQL> Skiis { get; set; }
        public DbSet<SkiBrandDBSQL> SkiBrands { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {
            Debug.WriteLine(System.Reflection.Assembly.GetEntryAssembly().Location);
            Database.EnsureCreated();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackStar.DataContext
{
    internal class AppDataContextFactory : IDesignTimeDbContextFactory<AppDataContext>
    {
        public AppDataContext CreateDbContext(string[] args)
        {
            string root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string DB_PATH = System.IO.Path.Combine(root, "TrackStar", "trackstar.db");

 
            Directory.CreateDirectory(Path.GetDirectoryName(DB_PATH)!);

            var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
            optionsBuilder.UseSqlite($"Data Source={DB_PATH}");

            return new AppDataContext(optionsBuilder.Options);
        }
    }
}


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackStar.Models.Entity;

namespace TrackStar.DataContext
{
    public class AppDataContext : DbContext
    {
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<SeriesEntity> Series { get; set; }
        public DbSet<MovieRatingEntity> MovieRatingEntities { get; set; }
        public DbSet<SeriesRatingEntity> SeriesRatingEntities { get; set; }
        
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }
    }
}

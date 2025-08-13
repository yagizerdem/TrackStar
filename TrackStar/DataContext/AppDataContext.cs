using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackStar.DataContext
{
    public class AppDataContext : DbContext
    {

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackStar.DataContext;
using TrackStar.Models.Entity;

namespace TrackStar.Services
{
    public class SavedService
    {
        private readonly AppDataContext _context;
        public SavedService()
        {
            _context = App.Services.GetService<AppDataContext>()!;
        }
    

        public async Task SaveMovie(MovieEntity entity)
        {
            // check if the movie already exists

            var MovieFromDb = _context.Movies.FirstOrDefault(m => m.ImdbID == entity.ImdbID || m.Title == entity.Title);
            
            bool flag = entity.Equals(MovieFromDb);

            if(flag && MovieFromDb.IsStarred) throw new ApplicationException("Movie is already saved.");

            if (!flag)
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                entity.IsStarred = true;
                await _context.Movies.AddAsync(entity);
            }
            else
            {
                MovieFromDb.UpdatedAt = DateTime.Now;
                MovieFromDb.IsStarred = false;
                _context.Movies.Update(MovieFromDb);
            }

                await _context.SaveChangesAsync();
        }
 
        public async Task<List<MovieEntity>> GetSavedMovies()
        {
            var entity = await _context.Movies.Where(m => m.IsStarred)
                .Include(m => m.Ratings)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            return entity;
        }
    
        public async Task SaveSeries(SeriesEntity entity)
        {
            // check if the series already exists
            var SeriesFromDb = _context.Series.FirstOrDefault(s => s.ImdbID == entity.ImdbID || s.Title == entity.Title);
            
            bool flag = entity.Equals(SeriesFromDb);
            if(flag && SeriesFromDb.IsStarred) throw new ApplicationException("Series is already saved.");
            if (!flag)
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                entity.IsStarred = true;
                await _context.Series.AddAsync(entity);
            }
            else
            {
                SeriesFromDb.UpdatedAt = DateTime.Now;
                SeriesFromDb.IsStarred = false;
                _context.Series.Update(SeriesFromDb);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<SeriesEntity>> GetSavedSeries()
        {
            var entity = await _context.Series.Where(s => s.IsStarred)
                .Include(s => s.Ratings)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
            return entity;
        }   
    }
}

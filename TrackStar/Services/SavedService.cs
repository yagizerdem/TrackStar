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
    }
}

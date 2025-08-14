using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackStar.DataContext;
using TrackStar.Models.Entity;

namespace TrackStar.Services
{
    public class WatchListService
    {
        private readonly AppDataContext _context;
        public WatchListService()
        {
            _context = App.Services.GetService<AppDataContext>()!;
        }


        public async Task AddMovieToWatchList(MovieEntity entity)
        {
            // check if the movie already exists in watchlist
            var movieFromDb = _context.Movies.FirstOrDefault(m => m.ImdbID == entity.ImdbID || m.Title == entity.Title);
            if (movieFromDb != null && movieFromDb.IsInWatchlist)
                throw new ApplicationException("Movie is already in watchlist.");
            if (movieFromDb == null)
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                entity.IsInWatchlist = true;
                await _context.Movies.AddAsync(entity);
            }
            else
            {
                movieFromDb.UpdatedAt = DateTime.Now;
                movieFromDb.IsInWatchlist = true;
                _context.Movies.Update(movieFromDb);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<MovieEntity>> GetMovieWatchListMovies()
        {
            var entity = await _context.Movies.Where(m => m.IsInWatchlist)
                .Include(m => m.Ratings)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
            return entity;
        }

        public async Task AddSeriesToWatchList(SeriesEntity entity)
        {
            // check if the series already exists in watchlist
            var seriesFromDb = _context.Series.FirstOrDefault(m => m.ImdbID == entity.ImdbID || m.Title == entity.Title);
            if (seriesFromDb != null && seriesFromDb.IsInWatchlist)
                throw new ApplicationException("Series is already in watchlist.");
            if (seriesFromDb == null)
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                entity.IsInWatchlist = true;
                await _context.Series.AddAsync(entity);
            }
            else
            {
                seriesFromDb.UpdatedAt = DateTime.Now;
                seriesFromDb.IsInWatchlist = true;
                _context.Series.Update(seriesFromDb);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<SeriesEntity>> GetSeriesWatchList()
        {
            var entity = await _context.Series.Where(m => m.IsInWatchlist)
                .Include(m => m.Ratings)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
            return entity;
        }
    
    }
}

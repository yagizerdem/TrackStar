using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Windows.Controls;
using TrackStar.Models.DTO;

namespace TrackStar.Services
{
    public class SeriesService
    {

        private readonly NetworkService _networkService;
        public SeriesService()
        {
            _networkService = App.Services.GetService<NetworkService>()!;
        }


        public async Task<List<OmdbSeriesDTO>> GetSeries(string title, int page)
        {
            try
            {
                string OMDB_API_KEY = Environment.GetEnvironmentVariable("OMDB_API_KEY")!;
                if (string.IsNullOrEmpty(OMDB_API_KEY))
                    throw new InvalidOperationException("OMDB_API_KEY environment variable is not set.");

                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Title cannot be null or empty.", nameof(title));

                string url = $"https://www.omdbapi.com/?apikey={OMDB_API_KEY}&s={Uri.EscapeDataString(title)}&type=series&page={page}";
                var jsonRespon = await _networkService.RequestWithRetry(url);
                
                OmdbSearchSeriesResponse response = JsonSerializer.Deserialize<OmdbSearchSeriesResponse>(jsonRespon, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                return response.Search;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<OmdbSeriesDetailsDTO> GetSeriesDetail(string title)
        {
            try
            {
                string OMDB_API_KEY = Environment.GetEnvironmentVariable("OMDB_API_KEY")!;
                if (string.IsNullOrEmpty(OMDB_API_KEY))
                    throw new InvalidOperationException("OMDB_API_KEY environment variable is not set.");

                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Title cannot be null or empty.", nameof(title));

                string url = $"https://www.omdbapi.com/?apikey={OMDB_API_KEY}&t={Uri.EscapeDataString(title)}&type=series";
                var jsonRespon = await _networkService.RequestWithRetry(url);

                OmdbSeriesDetailsDTO omdbSeriesDetailsDTO = JsonSerializer.Deserialize<OmdbSeriesDetailsDTO>(jsonRespon, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                return omdbSeriesDetailsDTO;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

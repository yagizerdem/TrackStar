using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrackStar.Models.DTO;

namespace TrackStar.Services
{
    public class MovieService
    {
        private readonly NetworkService _networkService;
        public MovieService() {
            _networkService = App.Services.GetService<NetworkService>()!;

        }

        public async Task<OmdbSearchMovieResponse> GetRecommendations()
        {
            string OMDB_API_KEY = Environment.GetEnvironmentVariable("OMDB_API_KEY")!;
            if (string.IsNullOrEmpty(OMDB_API_KEY))
                throw new InvalidOperationException("OMDB_API_KEY environment variable is not set.");

            Random rng = new Random();

            string[] SEEDS = {
        "love", "time", "star", "city", "night", "man", "dark", "world", "life", "death",
        "king", "home", "water", "fire", "earth", "space", "sun", "moon", "blood", "heart",
        "black", "white", "red", "blue", "green", "gold", "silver", "angel", "devil", "ghost",
        "secret", "last", "first", "american", "english", "summer", "winter", "spring", "fall",
        "mountain", "river", "forest", "ocean", "island", "road", "house", "family", "friend"
    };

            var uniqueMovies = new Dictionary<string, OmdbMovieDTO>(); // Key is ImdbID or Title
            var uniqueTitles = new HashSet<string>(); // Fallback for when ImdbID is null
            int targetCount = 10;
            int safetyLimit = 50;

            for (int attempt = 0; attempt < safetyLimit && uniqueMovies.Count < targetCount; attempt++)
            {
                string seed = SEEDS[rng.Next(SEEDS.Length)];
                int page = rng.Next(1, 6);

                string url = $"https://www.omdbapi.com/?apikey={OMDB_API_KEY}&s={Uri.EscapeDataString(seed)}&type=movie&page={page}";

                string jsonResponse;
                try
                {
                    jsonResponse = await _networkService.RequestWithRetry(url);
                    if (string.IsNullOrWhiteSpace(jsonResponse))
                        continue;
                }
                catch
                {
                    continue;
                }

                OmdbSearchMovieResponse? result;
                try
                {
                    result = JsonSerializer.Deserialize<OmdbSearchMovieResponse>(jsonResponse);
                    if (result == null)
                        continue;
                }
                catch
                {
                    continue;
                }

                if (result.Response?.Equals("True", StringComparison.OrdinalIgnoreCase) != true ||
                    result.Search == null ||
                    result.Search.Count == 0)
                {
                    continue;
                }

                foreach (var movie in result.Search)
                {
                    if (movie == null)
                        continue;

                    // Skip if we don't have at least a title
                    if (string.IsNullOrWhiteSpace(movie.Title))
                        continue;

                    // If we have an IMDB ID, use that as the primary key
                    if (!string.IsNullOrWhiteSpace(movie.ImdbID))
                    {
                        if (!uniqueMovies.ContainsKey(movie.ImdbID))
                        {
                            uniqueMovies[movie.ImdbID] = movie;
                            uniqueTitles.Add(movie.Title.ToLowerInvariant());
                            Console.WriteLine($"Found: {movie.Title} ({movie.Year}) [ID: {movie.ImdbID}]");

                            if (uniqueMovies.Count >= targetCount)
                                break;
                        }
                    }
                    // Fallback to title comparison if no IMDB ID
                    else
                    {
                        string lowerTitle = movie.Title.ToLowerInvariant();
                        if (!uniqueTitles.Contains(lowerTitle))
                        {
                            // Use title as the key in the dictionary
                            uniqueMovies[movie.Title] = movie;
                            uniqueTitles.Add(lowerTitle);
                            Console.WriteLine($"Found: {movie.Title} ({movie.Year}) [No ID]");

                            if (uniqueMovies.Count >= targetCount)
                                break;
                        }
                    }
                }
            }

            return new OmdbSearchMovieResponse
            {
                Response = uniqueMovies.Count > 0 ? "True" : "False",
                Search = uniqueMovies.Values.ToList(),
                TotalResults = uniqueMovies.Count.ToString()
            };
        }

    }
}

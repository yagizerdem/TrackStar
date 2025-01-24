using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TrackStar.MVVM.Models;

namespace TrackStar.MVVM.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private string apiKey { get; set; } = String.Empty;
        private readonly HttpClient client = new HttpClient();
        
        private Movie _movie;
        public Movie Movie
        {
            get => _movie;
            set
            {
                _movie = value;
                OnPropertyChanged(nameof(Movie));
            }
        }

        private MovieSearchList _searchList;

        public MovieSearchList SearchList
        {
            get => _searchList;
            set
            {
                _searchList = value;
                OnPropertyChanged(nameof(SearchList));
            }
        }

        public HomeViewModel()
        {
            // Open current application configuration
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection section = config.AppSettings.Settings;
            this.apiKey = section["ombdApiKey"].Value;
        }

        public async Task FetchData()
        {
            try
            {
                string[] genres = new string[]
                {
    "Horror",
    "Comedy",
    "Action",
    "Drama",
    "Thriller",
    "Adventure",
    "Fantasy",
    "Sci-Fi",
    "Mystery",
    "Romance",
    "Animation",
    "Documentary",
    "Biography",
    "Crime",
    "Western",
    "Musical",
    "War",
    "Family",
    "History",
    "Sports",
    "Superhero",
    "Psychological",
    "Epic",
    "Noir",
    "Cyberpunk",
    "Steampunk",
    "Parody",
    "Dark Comedy"
                };
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection section = config.AppSettings.Settings;
                string apiKey = section["ombdApiKey"].Value;

                Random random = new Random();
                string randomGenre = genres.ToList().OrderBy(x=> random.Next() - random.Next()).First();



                var str= $"http://www.omdbapi.com/?s={randomGenre}&apikey={apiKey}";
                using HttpResponseMessage response = await client.GetAsync($"http://www.omdbapi.com/?s={randomGenre}&apikey={apiKey}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                MovieSearchList MovieSearchList = MovieSearchList.FromJson(responseBody);

                Search FirstSearch = MovieSearchList.Search.First();

                using HttpResponseMessage response_ = await client.GetAsync($"http://www.omdbapi.com/?i={FirstSearch.imdbID}&apikey={apiKey}");
                response_.EnsureSuccessStatusCode();
                responseBody = await response_.Content.ReadAsStringAsync();

                Movie movie = Movie.FromJson(responseBody);

                // trigger ui update
                this.SearchList = MovieSearchList;
                this.Movie = movie;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }


        public async Task Initilize()
        {
            await FetchData();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using TrackStar.Models.DTO;
using TrackStar.Services;

namespace TrackStar.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        private  bool _isLoading = false;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }   


        private readonly AppService _appService;

        private readonly MovieService _movieService;
        

        public HomeViewModel()
        {
            _appService = App.Services.GetRequiredService<AppService>();
            _movieService = App.Services.GetRequiredService<MovieService>();

            Task.Run(async () => await FetchInitialData());
        }
    
        public async Task FetchInitialData()
        {
            try
            {
                IsLoading = true;
                await Task.WhenAll(GetMovieRecomendations());

            }
            catch(Exception ex)
            {
                Console.Write($"Execption occured {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

   
        public async Task GetMovieRecomendations()
        {
           OmdbSearchMovieResponse response =  await _movieService.GetRecommendations();

            ;
        }

    }
}

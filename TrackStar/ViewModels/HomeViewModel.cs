using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using TrackStar.Commands;
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

        public ObservableCollection<OmdbMovieDTO> Movies { get; set; } = new();


        private OmdbMovieDetailsDTO? _selecteDetails;

        public OmdbMovieDetailsDTO? SelectedDetails
        {
            get => _selecteDetails;
            set => SetProperty(ref _selecteDetails, value);
        }

        // commands
        public RelayCommand<OmdbMovieDTO> ShowDetailsCommand { get; set; }

        public  RelayCommand<object> CancelCommand { get; set; }

        public HomeViewModel()
        {
            _appService = App.Services.GetRequiredService<AppService>();
            _movieService = App.Services.GetRequiredService<MovieService>();

            Task.Run(async () => await FetchInitialData());

            ShowDetailsCommand = new RelayCommand<OmdbMovieDTO>(async (obj) => await ShowDetailsExecute(obj), _ => true);
            CancelCommand = new RelayCommand<object>(_ => SelectedDetails = null, _ => true);
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
            if (response.Search != null)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    response.Search.ForEach(m => Movies.Add(m));
                });
            }
        }


        public async Task ShowDetailsExecute(OmdbMovieDTO  dto)
        {
            try
            {
                IsLoading = true;
                OmdbMovieDetailsDTO response = await _movieService.GetMovieDetailsByTitle(dto.Title);
                SelectedDetails = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error showing details: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }

        }
    }
}

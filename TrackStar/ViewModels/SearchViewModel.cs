using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Reflection.Emit;
using TrackStar.Commands;
using TrackStar.Models.DTO;
using TrackStar.Services;

namespace TrackStar.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    SetProperty(ref _searchText, value);
                }
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    SetProperty(ref _isLoading, value);
                }
            }
        }

        private bool _isMovieMode = true;
        public bool IsMovieMode
        {
            get => _isMovieMode;
            set
            {
                SetProperty(ref _isMovieMode, value);
                OnPropertyChanged(nameof(IsSeriesMode));
            }
        }

        public bool IsSeriesMode => !IsMovieMode;

        private OmdbMovieDetailsDTO? _selecteDetails;

        public OmdbMovieDetailsDTO? SelectedDetails
        {
            get => _selecteDetails;
            set => SetProperty(ref _selecteDetails, value);
        }

        private OmdbSeriesDetailsDTO? _selectedSeriesDetails;

        public OmdbSeriesDetailsDTO? SelectedSeriesDetails
        {
            get => _selectedSeriesDetails;
            set => SetProperty(ref _selectedSeriesDetails, value);
        }

        private int moviePage = 1;

        private int seriesPage = 1; 

        private readonly MovieService _movieService;
        
        private readonly SeriesService _seriesService;

        public RelayCommand<object> SearchMoviesCommand { get; set; }
        
        public RelayCommand<object> SearchDetailsOfMovieCommand { get; set; }

        public RelayCommand<object> CloseMoveiDetailsCommand { get; set; }

        public RelayCommand<object> LoadMoreMoviesCommand { get; set; }   


        public RelayCommand<object> SearchSeriesCommand { get; set; }

        public RelayCommand<object> SearchDetailsOfSeriesCommand { get; set; }
        
        public RelayCommand<object> LoadMoreSeriesCommand { get; set; }

        public RelayCommand<object> CloseSeriesDetailsCommand { get; set; }

        public ObservableCollection<OmdbMovieDTO> SearchMovieResults { get; set; } = new ObservableCollection<OmdbMovieDTO>();

        public ObservableCollection<OmdbSeriesDTO> SearchSeriesResults { get; set; } = new ObservableCollection<OmdbSeriesDTO>();


        public SearchViewModel()
        {
            
            _movieService = App.Services.GetService<MovieService>()!;
            _seriesService = App.Services.GetService<SeriesService>()!;


            SearchMoviesCommand = new RelayCommand<object>(_ => SearchMoviesCommandExecute(), _ => !string.IsNullOrEmpty(SearchText) || !string.IsNullOrWhiteSpace(SearchText));
            SearchDetailsOfMovieCommand = new RelayCommand<object>(movie => SearchDetailsOfMovieExecute((OmdbMovieDTO)movie), movie => movie is OmdbMovieDTO);
            CloseMoveiDetailsCommand = new RelayCommand<object>(_ => SelectedDetails = null, _ => true);
            LoadMoreMoviesCommand = new RelayCommand<object>(_ => LoadMoreMoviesCommandExecute(), _ => !IsLoading && SearchMovieResults.Count > 0);
            SearchSeriesCommand = new RelayCommand<object>(_ => SearchSeriesCommandExecute(), _ => !string.IsNullOrEmpty(SearchText) || !string.IsNullOrWhiteSpace(SearchText));
            SearchDetailsOfSeriesCommand = new RelayCommand<object>(series => SearchDetailsOfSeriesCommandExecute((OmdbSeriesDTO)series), _ => !IsLoading &&  SearchSeriesResults.Count > 0);
            LoadMoreSeriesCommand = new RelayCommand<object>(_ => LoadMoreSeriesCommandExecute(), _ => !IsLoading && SearchSeriesResults.Count > 0);
            CloseSeriesDetailsCommand = new RelayCommand<object>(_ => SelectedSeriesDetails = null, _ => true);
        }

        async public void SearchMoviesCommandExecute()
        {
            try
            {
                SearchMovieResults.Clear();

                IsLoading = true;
                moviePage = 1;
                OmdbSearchMovieResponse response = await _movieService.SearchMoviesByTitleAsync(SearchText, moviePage);

                response.Search?.ForEach(m => SearchMovieResults.Add(m));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error executing search command: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        async public void SearchDetailsOfMovieExecute(OmdbMovieDTO movie)
        {
            try
            {
                IsLoading = true;
                OmdbMovieDetailsDTO response = await _movieService.GetMovieDetailsByTitle(movie.Title);
                if (response != null)
                {
                    SelectedDetails = response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching movie details: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    
        async public void LoadMoreMoviesCommandExecute()
        {
            try
            {
                IsLoading = true;
                moviePage++;

                OmdbSearchMovieResponse response = await _movieService.SearchMoviesByTitleAsync(SearchText, moviePage);

                response.Search?.ForEach(m => SearchMovieResults.Add(m));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading more movies: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        async public void SearchSeriesCommandExecute()
        {
            try
            {
                IsLoading = true;
                SearchSeriesResults.Clear();
                seriesPage = 1;
                List<OmdbSeriesDTO> resul = await _seriesService.GetSeries(SearchText, seriesPage);

                resul.ForEach(x => SearchSeriesResults.Add(x));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing search command: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }


        async public void SearchDetailsOfSeriesCommandExecute(OmdbSeriesDTO dto)
        {
            try
            {
                IsLoading = true;
                OmdbSeriesDetailsDTO response =  await _seriesService.GetSeriesDetail(dto.Title);
                SelectedSeriesDetails = response;
            }catch(Exception ex)
            {
                Console.WriteLine($"Error fetching series details: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        async public void LoadMoreSeriesCommandExecute()
        {
            try
            {
                IsLoading = true;
                seriesPage++;
                List<OmdbSeriesDTO> resul = await _seriesService.GetSeries(SearchText, seriesPage);

                resul.ForEach(x => SearchSeriesResults.Add(x));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading more series: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}

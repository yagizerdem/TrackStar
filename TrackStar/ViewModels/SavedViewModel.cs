using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using TrackStar.Commands;
using TrackStar.DataContext;
using TrackStar.Exceptions;
using TrackStar.Models.Entity;
using TrackStar.Services;
using TrackStar.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TrackStar.ViewModels
{
    public class SavedViewModel : ViewModelBase
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set { SetProperty(ref _isLoading, value); }
        }

        private string _query;

        public string Query
        {
            get => _query;
            set
            {
                SetProperty<string>(ref _query, value);
            }
        }

        private bool _isExactMatch;

        public bool IsExactMatch
        {
            get => _isExactMatch;
            set
            {
                if (value) IsSubstringMatch = false;
                SetProperty(ref _isExactMatch, value);
            }
        }

        private bool _isSubstringMatch;
        public bool IsSubstringMatch
        {
            get => _isSubstringMatch;
            set
            {
                if (value) IsExactMatch = false;
                SetProperty(ref _isSubstringMatch, value);
            }
        }

        private readonly SavedService _savedService;

        private readonly WatchListService _watchListService;

        // filter by

        // title
        private bool _filterByTitle;

        public bool FilterByTitle
        {
            get => _filterByTitle;
            set
            {
                if (value) _filterChanged?.Invoke();
                SetProperty(ref _filterByTitle, value);
            }
        }


        // director
        private bool _filterByDirector;

        public bool FilterByDirctor
        {
            get => _filterByDirector;
            set
            {
                if (value) _filterChanged?.Invoke();
                SetProperty(ref _filterByDirector, value);
            }
        }

        // writer

        private bool _filterByWriter;

        public bool FilterByWriter
        {
            get => _filterByWriter;
            set
            {
                if (value) _filterChanged?.Invoke();
                SetProperty(ref _filterByWriter, value);
            }
        }

        // actors
        private bool _filterByActors;

        public bool FilterByActors
        {
            get => _filterByActors;
            set
            {
                if (value) _filterChanged?.Invoke();
                SetProperty(ref _filterByActors, value);
            }
        }

        //Country
        private bool _filterByCountry;

        public bool FilterByCountry
        {
            get => _filterByCountry;
            set
            {
                if (value) _filterChanged?.Invoke();
                SetProperty(ref _filterByCountry, value);
            }
        }

        // filter changed event
        private event Action? _filterChanged;


        // sort by
        private bool _sortDescending;

        public bool SortDescending
        {
            get => _sortDescending;
            set
            {
                if (value) SortAscending = false;
                SetProperty(ref _sortDescending, value);
            }
        }

        private bool _sortAscending;
        public bool SortAscending
        {
            get => _sortAscending;
            set
            {
                if (value) SortDescending = false;
                SetProperty(ref _sortAscending, value);
            }
        }

        public ObservableCollection<string> SortByOptions { get; set; } = new ObservableCollection<string>
        {
            "Title",
            "Director",
            "Writer",
            "Actors",
            "Country",
            "Genre",
            "Created At",
            "Updated At",
            "Year",
        };

        public ObservableCollection<string> SortThenByOptions { get; set; } = new ObservableCollection<string>
        {
            "Title",
            "Director",
            "Writer",
            "Actors",
            "Country",
            "Genre",
            "Created At",
            "Updated At",
            "Year",
        };

        private string _sortBy;

        public string SortBy
        {
            get => _sortBy;
            set
            {
                SetProperty<string>(ref _sortBy, value);
            }
        }

        private string _sortThenBy;
        public string SortThenBy
        {
            get => _sortThenBy;
            set
            {
                SetProperty<string>(ref _sortThenBy, value);
            }
        }

        // view mode properties

        private bool _isGridView = true;
        private bool _isListView;
        private bool _isChanging; // guard flag

        public bool IsGridView
        {
            get => _isGridView;
            set
            {
                if (_isChanging) // prevent recursion
                {
                    _isGridView = value;
                    OnPropertyChanged();
                    return;
                }

                if (_isGridView != value)
                {
                    _isChanging = true;

                    _isGridView = value;
                    OnPropertyChanged();

                    if (value)
                        IsListView = false;
                    else if (!IsListView) // ensure at least one is true
                        IsListView = true;

                    _isChanging = false;
                }
            }
        }

        public bool IsListView
        {
            get => _isListView;
            set
            {
                if (_isChanging) // prevent recursion
                {
                    _isListView = value;
                    OnPropertyChanged();
                    return;
                }

                if (_isListView != value)
                {
                    _isChanging = true;

                    _isListView = value;
                    OnPropertyChanged();

                    if (value)
                        IsGridView = false;
                    else if (!IsGridView) // ensure at least one is true
                        IsGridView = true;

                    _isChanging = false;
                }
            }
        }


        // films - series selection

        private bool _isFilms = true;

        public bool IsFilms
        {
            get => _isFilms;
            set
            {
                SetProperty(ref _isFilms, value);
                OnPropertyChanged(nameof(IsSeries));
            }
        }

        public bool IsSeries => !IsFilms;

        private MovieEntity? _selectedMovie;
        public MovieEntity? SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                SetProperty(ref _selectedMovie, value);
            }
        }

        private SeriesEntity? _selectedSeries;
        public SeriesEntity? SelectedSeries
        {
            get => _selectedSeries;
            set
            {
                SetProperty(ref _selectedSeries, value);
            }

        }

        public ObservableCollection<MovieEntity> SavedMovies { get; set; } = new ObservableCollection<MovieEntity>();
        public List<MovieEntity> SavedMoviesClone { get; set; } = new();

        public ObservableCollection<SeriesEntity> SavedSeries { get; set; } = new ObservableCollection<SeriesEntity>();
        public List<SeriesEntity> SavedSeriesClone { get; set; } = new();

        //commands 
        public RelayCommand<object> RefreshCommand { get; set; }
        // show close details commands
        public RelayCommand<object> ShowMovieDetails { get; set; }
        public RelayCommand<object> ShowSeriesDetails { get; set; }
        public RelayCommand<object> CloseMoveiDetailsCommand { get; set; }
        public RelayCommand<object> CloseSeriesDetailsCommand { get; set; }

        public RelayCommand<object> ApplySortMovieCommand { get; set; }
        public RelayCommand<object> ApplySortingToSeriesCommand { get; set; }
        public RelayCommand<object> ApplyFilterMovieCommand { get; set; }
        public RelayCommand<object> ApplyFilterSeriesCommand { get; set; }

        public RelayCommand<object> AddMovieToWatchlist { get; set; }

        public RelayCommand<object> AddSeriesToWatchlist { get; set; }

        public RelayCommand<object> DelteMovieFromSavedCommand { get; set; }
    
        public RelayCommand<object> RemoveSeriesFromSavedCommand { get; set; }


        public SavedViewModel()
        {
            _savedService = App.Services.GetService<SavedService>()!;
            _watchListService = App.Services.GetService<WatchListService>()!;

            // fetch fromd db
            Initilize();
            // default panel settings
            IsExactMatch = true;
            IsListView = true;
            IsFilms = true;

            _filterChanged += () => DisableAllFilter();

            // assing command
            RefreshCommand = new RelayCommand<object>(async _ => await RefreshExecute(), _ => true);
            ShowMovieDetails = new RelayCommand<object>(m => ShowMovieDetailsExecute((MovieEntity)m), e => e.GetType() == typeof(MovieEntity));
            ShowSeriesDetails = new RelayCommand<object>(s => ShowSeriesDetailsExecute((SeriesEntity)s), e => e.GetType() == typeof(SeriesEntity));
            CloseMoveiDetailsCommand = new RelayCommand<object>(_ => SelectedMovie = null, _ => true);
            CloseSeriesDetailsCommand = new RelayCommand<object>(_ => SelectedSeries = null, _ => true);
            ApplySortMovieCommand = new RelayCommand<object>(_ => ApplySortMovieCommandExecute(), _ => true);
            ApplySortingToSeriesCommand = new RelayCommand<object>(_ => ApplySortSeriesCommandExecute(), _ => true);
            ApplyFilterMovieCommand = new RelayCommand<object>(_ => ApplyFilterMovieCommandExecute(), _ => true);
            ApplyFilterSeriesCommand = new RelayCommand<object>(_ => ApplyFilterSeriesCommandExecute(), _ => true);
            AddMovieToWatchlist = new RelayCommand<object>(async m => await AddMovieToWatchlistExecute((MovieEntity)m), m => m.GetType() == typeof(MovieEntity));
            AddSeriesToWatchlist = new RelayCommand<object>(async s => await AddSeriesToWatchlistExecute((SeriesEntity)s), s => s.GetType() == typeof(SeriesEntity));
            DelteMovieFromSavedCommand = new RelayCommand<object>(async m => await RemoveFilmFromSavedCommandExecute((MovieEntity)m), m => m.GetType() == typeof(MovieEntity));
            RemoveSeriesFromSavedCommand = new RelayCommand<object>(async s => await RemoveFilmFromSavedCommandExecute((SeriesEntity)s), s => s.GetType() == typeof(SeriesEntity));
        }

        public async Task Initilize()
        {
            try
            {
                IsLoading = true;

                await Task.WhenAll(
                    FetchFilms(),
                    FetchSeries()
                );


            }
            catch (ApplicationException ex)
            {
                ShowToast.ShowError(ex.Message ?? "Error occured");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie to watchlist: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void DisableAllFilter()
        {
            FilterByActors = false;
            FilterByCountry = false;
            FilterByDirctor = false;
            FilterByTitle = false;
            FilterByWriter = false;
        }

        private async Task FetchFilms()
        {
            try
            {
                var movies = await _savedService.GetSavedMovies();
                movies.ForEach(m => SavedMovies.Add(m));
                SavedMoviesClone = movies;
            }
            catch (AppException ex)
            {
                ShowToast.ShowError(ex.Message ?? "Error occured");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie to watchlist: {ex.Message}");
                ShowToast.ShowError("Error occured");
            }
        }
        private async Task FetchSeries()
        {
            try
            {
                var series = await _savedService.GetSavedSeries();
                series.ForEach(s => SavedSeries.Add(s));
                SavedSeriesClone = series;
            }
            catch (AppException ex)
            {
                ShowToast.ShowError(ex.Message ?? "Error occured");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding series to watchlist: {ex.Message}");
                ShowToast.ShowError("Error occured");
            }
        }

        private async Task RefreshExecute()
        {
            SavedMovies.Clear();
            SavedSeriesClone.Clear();
            SavedSeries.Clear();
            SavedSeriesClone.Clear();

            //SortAscending = false;
            //SortDescending = false;
            //SortBy = null!;
            //SortThenBy = null!;
            //Query = null!;
            await Initilize();
        }

        private void ShowMovieDetailsExecute(MovieEntity movie)
        {
            if (movie == null) return;
            SelectedMovie = movie;
        }
        private void ShowSeriesDetailsExecute(SeriesEntity series)
        {
            if (series == null) return;
            SelectedSeries = series;

        }



        private void ApplySortMovieCommandExecute()
        {
            if (string.IsNullOrEmpty(SortBy) || string.IsNullOrWhiteSpace(SortBy))
            {
                ShowToast.ShowError("Please select a valid sort by option.");
                return;
            }

            if (string.IsNullOrWhiteSpace(SortBy))
                return;

            // Primary sort
            IOrderedEnumerable<MovieEntity> sortedMovies = SortCollection(SavedMovies, SortBy);

            // Secondary sort if specified
            if (!string.IsNullOrWhiteSpace(SortThenBy) && SortThenBy != SortBy)
            {
                sortedMovies = SortCollectionThenBy(sortedMovies, SortThenBy);
            }

            // Update ObservableCollection
            var sortedList = sortedMovies.ToList();
            SavedMovies.Clear();
            foreach (var movie in sortedList)
                SavedMovies.Add(movie);

            if (!SortAscending)
            {
                SavedMovies.Reverse();
            }

        }

        private void ApplySortSeriesCommandExecute()
        {
            if (string.IsNullOrEmpty(SortBy) || string.IsNullOrWhiteSpace(SortBy))
            {
                ShowToast.ShowError("Please select a valid sort by option.");
                return;
            }
            if (string.IsNullOrWhiteSpace(SortBy))
                return;
            // Primary sort
            IOrderedEnumerable<SeriesEntity> sortedSeries = SortCollection(SavedSeries, SortBy);
            // Secondary sort if specified
            if (!string.IsNullOrWhiteSpace(SortThenBy) && SortThenBy != SortBy)
            {
                sortedSeries = SortCollectionThenBy(sortedSeries, SortThenBy);
            }
            // Update ObservableCollection
            var sortedList = sortedSeries.ToList();
            SavedSeries.Clear();
            foreach (var series in sortedList)
                SavedSeries.Add(series);
            if (!SortAscending)
            {
                SavedSeries.Reverse();
            }
        }

        // sort helpers
        private IOrderedEnumerable<T> SortCollection<T>(IEnumerable<T> source, string sortField)
        {
            return sortField switch
            {
                "Title" => source.OrderBy(m => GetPropertyValue(m, "Title")),
                "Director" => source.OrderBy(m => GetPropertyValue(m, "Director")),
                "Writer" => source.OrderBy(m => GetPropertyValue(m, "Writer")),
                "Actors" => source.OrderBy(m => GetPropertyValue(m, "Actors")),
                "Country" => source.OrderBy(m => GetPropertyValue(m, "Country")),
                "Genre" => source.OrderBy(m => GetPropertyValue(m, "Genre")),
                "Created At" => source.OrderBy(m => GetPropertyValue(m, "CreatedAt")),
                "Updated At" => source.OrderBy(m => GetPropertyValue(m, "UpdatedAt")),
                "Year" => source.OrderBy(m => GetPropertyValue(m, "Year")),
                _ => source.OrderBy(m => GetPropertyValue(m, "Title"))
            };
        }

        private IOrderedEnumerable<T> SortCollectionThenBy<T>(IOrderedEnumerable<T> source, string sortField)
        {
            return sortField switch
            {
                "Title" => source.ThenBy(m => GetPropertyValue(m, "Title")),
                "Director" => source.ThenBy(m => GetPropertyValue(m, "Director")),
                "Writer" => source.ThenBy(m => GetPropertyValue(m, "Writer")),
                "Actors" => source.ThenBy(m => GetPropertyValue(m, "Actors")),
                "Country" => source.ThenBy(m => GetPropertyValue(m, "Country")),
                "Genre" => source.ThenBy(m => GetPropertyValue(m, "Genre")),
                "Created At" => source.ThenBy(m => GetPropertyValue(m, "CreatedAt")),
                "Updated At" => source.ThenBy(m => GetPropertyValue(m, "UpdatedAt")),
                "Year" => source.ThenBy(m => GetPropertyValue(m, "Year")),
                _ => source
            };
        }

        private object GetPropertyValue<T>(T obj, string propertyName)
        {
            return typeof(T).GetProperty(propertyName)?.GetValue(obj, null);
        }
        //


        // filters
        private void ApplyFilterMovieCommandExecute()
        {
            IEnumerable<MovieEntity> filteredMovies = SavedMovies;

            if (FilterByTitle)
            {
                filteredMovies = IsExactMatch
                    ? filteredMovies.Where(x => string.Equals(x.Title, Query, StringComparison.OrdinalIgnoreCase))
                    : filteredMovies.Where(x => x.Title?.Contains(Query, StringComparison.OrdinalIgnoreCase) == true);
            }

            if (FilterByDirctor)
            {
                filteredMovies = IsExactMatch
                    ? filteredMovies.Where(x => string.Equals(x.Director, Query, StringComparison.OrdinalIgnoreCase))
                    : filteredMovies.Where(x => x.Director?.Contains(Query, StringComparison.OrdinalIgnoreCase) == true);
            }

            if (FilterByWriter)
            {
                filteredMovies = IsExactMatch
                    ? filteredMovies.Where(x => string.Equals(x.Writer, Query, StringComparison.OrdinalIgnoreCase))
                    : filteredMovies.Where(x => x.Writer?.Contains(Query, StringComparison.OrdinalIgnoreCase) == true);
            }

            if (FilterByActors)
            {
                filteredMovies = IsExactMatch
                    ? filteredMovies.Where(x => string.Equals(x.Actors, Query, StringComparison.OrdinalIgnoreCase))
                    : filteredMovies.Where(x => x.Actors?.Contains(Query, StringComparison.OrdinalIgnoreCase) == true);
            }

            if (FilterByCountry)
            {
                filteredMovies = IsExactMatch
                    ? filteredMovies.Where(x => string.Equals(x.Country, Query, StringComparison.OrdinalIgnoreCase))
                    : filteredMovies.Where(x => x.Country?.Contains(Query, StringComparison.OrdinalIgnoreCase) == true);
            }

            // Update collection without breaking UI binding
            var filteredList = filteredMovies.ToList();
            SavedMovies.Clear();
            foreach (var movie in filteredList)
                SavedMovies.Add(movie);
        }

        private void ApplyFilterSeriesCommandExecute()
        {
            IEnumerable<SeriesEntity> filteredSeries = SavedSeries;
            if (FilterByTitle)
            {
                filteredSeries = IsExactMatch
                    ? filteredSeries.Where(x => string.Equals(x.Title, Query, StringComparison.OrdinalIgnoreCase))
                    : filteredSeries.Where(x => x.Title?.Contains(Query, StringComparison.OrdinalIgnoreCase) == true);
            }
            if (FilterByDirctor)
            {
                filteredSeries = IsExactMatch
                    ? filteredSeries.Where(x => string.Equals(x.Director, Query, StringComparison.OrdinalIgnoreCase))
                    : filteredSeries.Where(x => x.Director?.Contains(Query, StringComparison.OrdinalIgnoreCase) == true);
            }
            if (FilterByWriter)
            {
                filteredSeries = IsExactMatch
                    ? filteredSeries.Where(x => string.Equals(x.Writer, Query, StringComparison.OrdinalIgnoreCase))
                    : filteredSeries.Where(x => x.Writer?.Contains(Query, StringComparison.OrdinalIgnoreCase) == true);
            }
            if (FilterByActors)
            {
                filteredSeries = IsExactMatch
                    ? filteredSeries.Where(x => string.Equals(x.Actors, Query, StringComparison.OrdinalIgnoreCase))
                    : filteredSeries.Where(x => x.Actors?.Contains(Query, StringComparison.OrdinalIgnoreCase) == true);
            }
            if (FilterByCountry)
            {
                filteredSeries = IsExactMatch
                    ? filteredSeries.Where(x => string.Equals(x.Country, Query, StringComparison.OrdinalIgnoreCase))
                    : filteredSeries.Where(x => x.Country?.Contains(Query, StringComparison.OrdinalIgnoreCase) == true);
            }
            // Update collection without breaking UI binding
            var filteredList = filteredSeries.ToList();
            SavedSeries.Clear();
            foreach (var series in filteredList)
                SavedSeries.Add(series);
        }

        // add watchlist 

        private async Task AddMovieToWatchlistExecute(MovieEntity entity)
        {
            if (entity == null) return;
            try
            {
                IsLoading = true;
                await _watchListService.AddMovieToWatchList(entity);
                ShowToast.ShowSuccess("Movie added to watchlist successfully.");
            }
            catch (TrackStar.Exceptions.AppException ex)
            {
                ShowToast.ShowError(ex.Message ?? "Error occured");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie to watchlist: {ex.Message}");
                ShowToast.ShowError("Error occured");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task AddSeriesToWatchlistExecute(SeriesEntity entity)
        {
            if (entity == null) return;
            try
            {
                IsLoading = true;
                await _watchListService.AddSeriesToWatchList(entity);
                ShowToast.ShowSuccess("Series added to watchlist successfully.");
            }
            catch (TrackStar.Exceptions.AppException ex)
            {
                ShowToast.ShowError(ex.Message ?? "Error occured");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding series to watchlist: {ex.Message}");
                ShowToast.ShowError("Error occured");
            }
            finally
            {
                IsLoading = false;
            }
        }


        // remove functions

        private async Task RemoveFilmFromSavedCommandExecute(MovieEntity entity)
        {
            if (entity == null) return;
            try
            {
                IsLoading = true;
                await _savedService.RemoveMovie(entity);
                SavedMovies.Remove(entity);

                // refresh gui
                this.SavedMovies.Clear();
                this.SavedMoviesClone = new List<MovieEntity>(this.SavedMoviesClone.Where(m => m.ImdbID != entity.ImdbID));
                this.SavedMoviesClone.ForEach(m => this.SavedMovies.Add(m));

                ShowToast.ShowSuccess("Movie removed from saved successfully.");
            }
            catch (TrackStar.Exceptions.AppException ex)
            {
                ShowToast.ShowError(ex.Message ?? "Error occured");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing movie from saved: {ex.Message}");
                ShowToast.ShowError("Error occured");
            }
            finally
            {
                IsLoading = false;
            }
        }
    
        private async Task RemoveFilmFromSavedCommandExecute(SeriesEntity entity)
        {
            if (entity == null) return;
            try
            {
                IsLoading = true;
                await _savedService.RemoveSeries(entity);
                SavedSeries.Remove(entity);
                // refresh gui
                this.SavedSeries.Clear();
                this.SavedSeriesClone = new List<SeriesEntity>(this.SavedSeriesClone.Where(s => s.ImdbID != entity.ImdbID));
                this.SavedSeriesClone.ForEach(s => this.SavedSeries.Add(s));
                
                ShowToast.ShowSuccess("Series removed from saved successfully.");
            }
            catch (TrackStar.Exceptions.AppException ex)
            {
                ShowToast.ShowError(ex.Message ?? "Error occured");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing series from saved: {ex.Message}");
                ShowToast.ShowError("Error occured");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
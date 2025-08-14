using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackStar.DataContext;
using TrackStar.Exceptions;
using TrackStar.Models.Entity;
using TrackStar.Services;
using TrackStar.Utils;

namespace TrackStar.ViewModels
{
    public  class SavedViewModel : ViewModelBase
    {
        private bool _isLoading;

        public  bool IsLoading
        {
            get => _isLoading;
            set { SetProperty(ref _isLoading, value); }
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

        // filter by
        
        // title
        private bool _filterByTitle;
        
        public bool FilterByTitle
        {
            get => _filterByTitle;
            set 
            {
                if(value) _filterChanged?.Invoke();
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
        private event Action ?  _filterChanged;


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

        public  ObservableCollection<string> SortByOptions { get; set; } = new ObservableCollection<string>
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


        public ObservableCollection<MovieEntity> SavedMovies { get; set; } = new ObservableCollection<MovieEntity>();
        public List<MovieEntity> SavedMoviesClone { get; set; } = new();
        public SavedViewModel()
        {
            _savedService = App.Services.GetService<SavedService>()!;

            Initilize();
            _filterChanged += () => DisableAllFilter();
        }


        public async Task Initilize()
        {
            try
            {
                IsLoading = true;
                IsExactMatch = true;
                SortAscending = true;
                IsListView = true;

                var movies = await _savedService.GetSavedMovies();
                movies.ForEach(m => SavedMovies.Add(m));
                SavedMoviesClone = movies;

            }
            catch (ApplicationException ex)
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

        private void DisableAllFilter()
        {
            FilterByActors = false;
            FilterByCountry = false;
            FilterByDirctor = false;
            FilterByTitle = false;
            FilterByWriter = false;
        }
    }
}

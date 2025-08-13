using Microsoft.Extensions.DependencyInjection;
using TrackStar.Commands;
using TrackStar.Services;

namespace TrackStar.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set { SetProperty(ref _isLoading, value); }
        }

        public RelayCommand<object> NavigateHome { get; set; }

        public RelayCommand<object> NavigateSearch { get; set; }

        public RelayCommand<object> NavigateSaved { get; set; }

        
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel != value)
                {
                    SetProperty(ref _currentViewModel, value);
                }
            }
        }

        private ViewModelBase _homeViewModel;

        private ViewModelBase _searchViewModel;

        private ViewModelBase _savedViewModel;

        private readonly AppService _appService;
        public MainViewModel() {

            _appService = App.Services.GetRequiredService<AppService>();

            //_homeViewModel = new HomeViewModel();
            _searchViewModel = new SearchViewModel();


            NavigateHome = new RelayCommand<object>(o => NavigateHomeExecute(), _ => true);
            NavigateSearch = new RelayCommand<object>(o => NavigateSearchExecute(), _ => true);

            CurrentViewModel = _searchViewModel;

            // assing event callbacks
            _appService.OnLoadStateChaged += _ => IsLoading = _appService.IsLoading;  
        }


        public void NavigateHomeExecute() => CurrentViewModel = _homeViewModel;
        public void NavigateSearchExecute() => CurrentViewModel = _searchViewModel;
    

    }
}

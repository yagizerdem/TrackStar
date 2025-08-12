using TrackStar.Commands;

namespace TrackStar.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

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
        public MainViewModel() {

            _homeViewModel = new HomeViewModel();
            _searchViewModel = new SearchViewModel();


            NavigateHome = new RelayCommand<object>(o => NavigateHomeExecute(), _ => true);
            NavigateSearch = new RelayCommand<object>(o => NavigateSearchExecute(), _ => true);

            CurrentViewModel = _homeViewModel;
        }


        public void NavigateHomeExecute() => CurrentViewModel = _homeViewModel;
        public void NavigateSearchExecute() => CurrentViewModel = _searchViewModel;
    

    }
}

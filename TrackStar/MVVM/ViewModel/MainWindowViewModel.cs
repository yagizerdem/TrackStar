using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrackStar.Commands;

namespace TrackStar.MVVM.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateHomePanelCommand { get; }
        public ICommand NavigateSettingsPanelCommand { get; }
        public ICommand NavigateSearchPanelCommand { get; }

        public MainWindowViewModel()
        {
            // Initialize Commands
            NavigateHomePanelCommand = new RelayCommand(() => { CurrentViewModel = new HomeViewModel(); });
            NavigateSettingsPanelCommand = new RelayCommand(() => { CurrentViewModel = new SettingsViewModel(); });
            NavigateSearchPanelCommand = new RelayCommand(() => { CurrentViewModel = new SearchViewModel(); });

            // Default View
            CurrentViewModel = new HomeViewModel();
        }

    }
}

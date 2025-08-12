using Microsoft.Extensions.DependencyInjection;
using TrackStar.Services;

namespace TrackStar.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly NetworkService _networkService;
        public HomeViewModel()
        {
            _networkService = App.Services.GetRequiredService<NetworkService>();

            Task.Run(async () => await FetchInitialData());
        }
    
        public async Task FetchInitialData()
        {
            ;
        }
    }
}

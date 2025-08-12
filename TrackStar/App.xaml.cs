using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TrackStar.Services;

namespace TrackStar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static IServiceProvider Services { get; private set; } = null!;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Env.Load(); // load .api keys from file

            SetUpApiKey(); 


            RegisterServices(); 
        }


        private void SetUpApiKey()
        {

            string apiKey = Env.GetString("TMDB_API_KEY");
            Environment.SetEnvironmentVariable("TMDB_API_KEY", apiKey);
        }

        protected void RegisterServices()
        {
            // Register your services here
            ServiceCollection services = new();

            // add scope managements
            services.AddSingleton<NetworkService>();

            // build service provider
            ServiceProvider provider = services.BuildServiceProvider();
            Services = provider;
        }

    }

}

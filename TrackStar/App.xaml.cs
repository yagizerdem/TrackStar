using AutoMapper;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using TrackStar.DataContext;
using TrackStar.Services;

namespace TrackStar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static IServiceProvider Services { get; private set; } = null!;

        public static IMapper mapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Env.Load(); // load .api keys from file

            SetUpApiKey();
            SetUpDb();

            RegisterServices(); 
        }


        private void SetUpApiKey()
        {

            string apiKey = Env.GetString("OMDB_API_KEY");
            Environment.SetEnvironmentVariable("OMDB_API_KEY", apiKey);
        }

        private void RegisterServices()
        {
            // Register your services here
            ServiceCollection services = new();

            // add scope managements
            services.AddSingleton<NetworkService>();
            services.AddSingleton<AppService>();
            services.AddSingleton<MovieService>();
            services.AddSingleton<SeriesService>();
            services.AddSingleton<SavedService>();
            services.AddSingleton<WatchListService>();

            services.AddDbContext<AppDataContext>(options =>
            {
                var dbPath = Environment.GetEnvironmentVariable("DB_PATH");
                options.UseSqlite($"Data Source={dbPath}");
            });


            // add mapper config
            MapperConfiguration _config = ConfigureAutoMapper.Configure();
            mapper = _config.CreateMapper();
            

            // build service provider
            ServiceProvider provider = services.BuildServiceProvider();
            Services = provider;
        }

        private void SetUpDb()
        {
            string root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string DB_PATH = System.IO.Path.Combine(root, "TrackStar", "trackstar.db");

            if (!File.Exists(DB_PATH))
            {
                File.Create(DB_PATH).Close();   
            }

            Environment.SetEnvironmentVariable("DB_PATH", DB_PATH); 
        }

    }

}

using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace TrackStar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            try
            {
                // Open current application configuration
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection section = config.AppSettings.Settings;

                if (section["ombdApiKey"] == null) section.Add("ombdApiKey", "b6054d9a");
                else section["ombdApiKey"].Value = "b6054d9a";


                // initlize db

                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string fullPath = Path.Join(basePath, "appService.db");
                if (!File.Exists(fullPath))
                {
                    File.Create(fullPath);
                }
                if (section["appServicePath"] == null) section.Add("appServicePath", fullPath);
                else section["appServicePath"].Value = fullPath;

                // Save changes to file
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }
            catch(Exception ex)
            {
                string message = $"error is {ex.Message}";
                Console.WriteLine(message);
            }

            base.OnStartup(e);
        }
    }

}

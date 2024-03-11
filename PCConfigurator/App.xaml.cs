using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.Stores;
using PCConfigurator.ViewModel;
using System.Windows;

namespace PCConfigurator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationContext dbContext = new ApplicationContext();
            NavigationStorage navigationStorage = new NavigationStorage();
            MainViewModel mainViewModel = new MainViewModel(navigationStorage);
            MainWindow = new MainWindow() { DataContext = mainViewModel };
            MainWindow.Show();
        }
    }

}

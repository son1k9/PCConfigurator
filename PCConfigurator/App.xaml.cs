using Microsoft.EntityFrameworkCore;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.Model.Components.M2;
using PCConfigurator.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace PCConfigurator;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        ApplicationContext applicationContext = new ApplicationContext();
        applicationContext.Database.Migrate();
        MainViewModel mainViewModel = new MainViewModel();
        MainWindow = new MainWindow() { DataContext = mainViewModel };
        MainWindow.Show();
    }
}

using PCConfigurator.ViewModel;
using System.Windows;

namespace PCConfigurator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    MainViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            _viewModel = viewModel;
            viewModel.NavigationCanceled += ChangeBack;
        }
    }

    private void ChangeBack(object? sender, EventArgs args)
    {
        RadioButtonConfigurations.IsChecked = true;
        RadioButtonComponents.IsChecked = false;
    }
}
using PCConfigurator.Commands;
using PCConfigurator.Stores;
using System.Windows;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

internal class MainViewModel : BaseViewModel
{
    private BaseViewModel _currentViewModel = new ConfigurationsViewModel();
    public BaseViewModel CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    public event EventHandler? NavigationCanceled;
    private void OnNavigationCanceled()
    {
        NavigationCanceled?.Invoke(this, EventArgs.Empty);
    }
    


    public MainViewModel(NavigationStorage navigationStorage)
    {

    }

    private RelayCommand? _navigateConfigurations;
    public ICommand NavigateConfigurations => _navigateConfigurations ?? new RelayCommand(PerformNavigateConfigurations);
    private void PerformNavigateConfigurations(object? commandParameter)
    {
        if (CurrentViewModel is ConfigurationsViewModel)
            return;

        CurrentViewModel = new ConfigurationsViewModel();
    }

    private RelayCommand? _navigateComponents;
    public ICommand NavigateComponents => _navigateComponents ?? new RelayCommand(PerformNavigateComponents);
    private void PerformNavigateComponents(object? commandParameter)
    {
        if (CurrentViewModel is ComponentsViewModel)
            return;

        if (CurrentViewModel is ConfigurationsViewModel configurationsViewModel)
        {
            if (configurationsViewModel.SelectedConfiguration?.Changes == true)
            {
                var result = MessageBox.Show("Сохранить изменения?", "", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    configurationsViewModel.SelectedConfiguration.Save.Execute(null);
                else if (result == MessageBoxResult.Cancel)
                {
                    OnNavigationCanceled();
                    return;
                }
            }
        }

        CurrentViewModel = new ComponentsViewModel();
    }
}
using PCConfigurator.Commands;
using System.Windows;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

/// <summary>
/// Class that represents ViewModel for main window
/// </summary>
public class MainViewModel : BaseViewModel
{
    private int lastConfigurationid = 0;

    private BaseViewModel _currentViewModel = new ConfigurationsViewModel();
    /// <summary>
    /// Current chosen ViewModel
    /// </summary>
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
    /// <summary>
    /// Raises NavigationCanceled event
    /// </summary>
    private void OnNavigationCancel()
    {
        NavigationCanceled?.Invoke(this, EventArgs.Empty);
    }

    private RelayCommand? _navigateConfigurations;
    /// <summary>
    /// Command to navigate to configurations
    /// </summary>
    public ICommand NavigateConfigurations => _navigateConfigurations ??= new RelayCommand(PerformNavigateConfigurations);
    /// <summary>
    /// Navigates to configurations by change CurrentViewModel to a ConfigurationsViewModel    
    /// </summary>
    /// <param name="commandParameter">Not used</param>
    private void PerformNavigateConfigurations(object? commandParameter)
    {
        if (CurrentViewModel is ConfigurationsViewModel)
            return;

        CurrentViewModel = new ConfigurationsViewModel(lastConfigurationid);
    }

    private RelayCommand? _navigateComponents;
    /// <summary>
    /// Command to navigate to configurations
    /// </summary>
    public ICommand NavigateComponents => _navigateComponents ??= new RelayCommand(PerformNavigateComponents);
    /// <summary>
    /// Navigates to components by change CurrentViewModel to a ComponentsViewModel    
    /// </summary>
    /// <param name="commandParameter">Not used</param>
    private void PerformNavigateComponents(object? commandParameter)
    {
        if (CurrentViewModel is ConfigurationsViewModel configurationsViewModel)
        {
            if (configurationsViewModel.SelectedConfiguration != null)
            {
                if (configurationsViewModel.SelectedConfiguration.Changes)
                {
                    var result = MessageBox.Show("Сохранить изменения?", "Закрытие конфигурации", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        configurationsViewModel.SelectedConfiguration.Save.Execute(null);
                        if (configurationsViewModel.SelectedConfiguration.Changes)
                        {
                            OnNavigationCancel();
                            return;
                        }
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        OnNavigationCancel();
                        return;
                    }
                }
                lastConfigurationid = configurationsViewModel.SelectedConfiguration.Configuration.ConfigurationId;
            }
            else
            {
                lastConfigurationid = 0;
                CurrentViewModel = new ComponentsViewModel();
            }
        }
    }
}

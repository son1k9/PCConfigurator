using PCConfigurator.Commands;
using System.Windows;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

internal class MainViewModel : BaseViewModel
{
    private int lastConfigurationid = 0;

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
    private void OnNavigationCancel()
    {
        NavigationCanceled?.Invoke(this, EventArgs.Empty);
    }

    private RelayCommand? _navigateConfigurations;
    public ICommand NavigateConfigurations => _navigateConfigurations ??= new RelayCommand(PerformNavigateConfigurations);
    private void PerformNavigateConfigurations(object? commandParameter)
    {
        if (CurrentViewModel is ConfigurationsViewModel)
            return;

        CurrentViewModel = new ConfigurationsViewModel(lastConfigurationid);
    }

    private RelayCommand? _navigateComponents;
    public ICommand NavigateComponents => _navigateComponents ??= new RelayCommand(PerformNavigateComponents);
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
                lastConfigurationid = 0;

            CurrentViewModel = new ComponentsViewModel();
        }
    }
}
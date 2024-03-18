using PCConfigurator.Commands;
using PCConfigurator.Stores;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

internal class MainViewModel : BaseViewModel
{
    private readonly NavigationStorage _navigationStorage;

    public BaseViewModel CurrentViewModel => _navigationStorage.CurrentViewModel;

    public MainViewModel(NavigationStorage navigationStorage)
    {
        _navigationStorage = navigationStorage;
        navigationStorage.ViewModelChanged += OnViewModelChanged;
        NavigateConfigurationsCommand = new NavigateCommand<ConfigurationsViewModel>(navigationStorage);
        NavigateComponentsCommand = new NavigateCommand<ComponentsViewModel>(navigationStorage);
    }

    private void OnViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    public ICommand NavigateConfigurationsCommand { get; }
    public ICommand NavigateComponentsCommand { get; }
}

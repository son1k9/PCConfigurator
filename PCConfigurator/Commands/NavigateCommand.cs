using PCConfigurator.Stores;
using System.Windows.Input;

namespace PCConfigurator.Commands;

internal class NavigateCommand<T>(NavigationStorage navigationStorage) : ICommand
    where T : ViewModel.BaseViewModel, new()
{
    private readonly NavigationStorage _navigationStorage = navigationStorage;

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (_navigationStorage.CurrentViewModel.GetType() == typeof(T))
            return;
        _navigationStorage.CurrentViewModel = new T();
    }
}

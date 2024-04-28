using PCConfigurator.ViewModel;
using PCConfigurator.ViewModel.ComponentsViewModels;
using System.Windows.Input;

namespace PCConfigurator.Commands;

internal class NavigateCommand<T>(ComponentsViewModel viewModel) : ICommand
    where T : ComponentViewModel, new()
{
    private ComponentsViewModel _viewModel = viewModel;

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (_viewModel.GetType() == typeof(T))
            return;
        _viewModel.CurrentViewModel = new T();
    }
}

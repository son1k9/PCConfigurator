using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

internal class NewCoolerViewmodel(Cooler cooler) : BaseViewModel
{
    public Cooler Cooler { get; } = cooler;

    private RelayCommand _removeSocket;
    public ICommand RemoveSocket => _removeSocket ??= new RelayCommand(PerformRemoveSocket);
    public void PerformRemoveSocket(object? commandParametr)
    {
        if (commandParametr is Socket socket)
            Cooler.Sockets.Remove(socket);
    }

    private RelayCommand _addSocket;
    public ICommand AddSocket => _addSocket ??= new RelayCommand(PerformAddSocket);
    private void PerformAddSocket(object? commandParameter)
    {
        if (commandParameter is Socket socket && !Cooler.Sockets.Contains(socket))
            Cooler.Sockets.Add(socket);
    }
}

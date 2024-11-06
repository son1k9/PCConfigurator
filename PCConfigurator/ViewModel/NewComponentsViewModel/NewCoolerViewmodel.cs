using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

/// <summary>
/// ViewModel for addition of a new cooler
/// </summary>
public class NewCoolerViewmodel : BaseViewModel
{
    /// <summary>
    /// Cooler that is being added
    /// </summary>
    public Cooler Cooler { get; }

    private RelayCommand _removeSocket;
    /// <summary>
    /// Command to remove Socket from a Cooler
    /// </summary>
    public ICommand RemoveSocket => _removeSocket ??= new RelayCommand(PerformRemoveSocket);

    /// <summary>
    /// Removes Socket from a Cooler
    /// </summary>
    /// <param name="commandParametr">Socket to remove</param>
    public void PerformRemoveSocket(object? commandParametr)
    {
        if (commandParametr is Socket socket)
            Cooler.Sockets.Remove(socket);
    }

    private RelayCommand _addSocket;

    /// <summary>
    /// Creates new NewCoolerViewmodel with given Cooler
    /// </summary>
    /// <param name="cooler">Cooler that is beging added</param>
    public NewCoolerViewmodel(Cooler cooler)
    {
        Cooler = cooler;
    }

    /// <summary>
    /// Command to add Socket to a Cooler
    /// </summary>
    public ICommand AddSocket => _addSocket ??= new RelayCommand(PerformAddSocket);

    /// <summary>
    /// Adds Socket to a Cooler
    /// </summary>
    /// <param name="commandParameter">Socket to add</param>
    private void PerformAddSocket(object? commandParameter)
    {
        if (commandParameter is Socket socket && !Cooler.Sockets.Contains(socket))
            Cooler.Sockets.Add(socket);
    }
}

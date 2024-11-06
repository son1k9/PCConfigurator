using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

/// <summary>
/// ViewModel for addition of a new socket
/// </summary>
public class NewSocketViewModel : BaseViewModel
{
    /// <summary>
    /// Socket that is being added
    /// </summary>
    public Socket Socket { get; }

    /// <summary>
    /// List of chipsets that were remove since the creation of this NewSocketViewModel
    /// </summary>
    public List<Chipset> RemovedChipsets { get; } = [];

    private RelayCommand _removeChipset;
    /// <summary>
    /// Command to remove Chipset from a Motherboard
    /// </summary>
    public ICommand RemoveChipset => _removeChipset ??= new RelayCommand(PerformRemoveChipset);
    /// <summary>
    /// Removes Chipset from a Socket
    /// </summary>
    /// <param name="commandParametr">Chipset to remove</param>
    public void PerformRemoveChipset(object? commandParametr)
    {
        if (commandParametr is Chipset chipset)
        {
            Socket.Chipsets.Remove(chipset);
            RemovedChipsets.Add(chipset);
        }
    }

    /// <summary>
    /// Creates new NewSocketViewModel with given socket
    /// </summary>
    /// <param name="socket">Motherboard that is beging added</param>
    public NewSocketViewModel(Socket socket)
    {
        Socket = socket;
    }

    private RelayCommand _addChipset;
    /// <summary>
    /// Command to add Chipset to a Socket
    /// </summary>
    public ICommand AddChipset => _addChipset ??= new RelayCommand(PerformAddChipset);
    /// <summary>
    /// Adds new Chipset to a Socket
    /// </summary>
    /// <param name="commandParametr">Not used</param>
    public void PerformAddChipset(object? commandParametr)
    {
        Socket.Chipsets.Add(new Chipset());
    }
}

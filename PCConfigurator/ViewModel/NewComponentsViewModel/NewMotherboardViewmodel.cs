using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using PCConfigurator.Model.Components.M2;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

/// <summary>
/// ViewModel for addition of a new motherboard
/// </summary>
public class NewMotherboardViewmodel : BaseViewModel
{
    /// <summary>
    /// Motherboard that is being added
    /// </summary>
    public Motherboard Motherboard { get; }

    /// <summary>
    /// List of M2 Slots that were remove since the creation of this NewMotherboardViewModel
    /// </summary>
    public List<M2Slot> RemovedM2Slots { get; } = [];

    private RelayCommand _removeM2Slot;
    /// <summary>
    /// Command to remove M2 Slot from a Motherboard
    /// </summary>
    public ICommand RemoveM2Slot => _removeM2Slot ??= new RelayCommand(PerformRemoveM2Slot);

    /// <summary>
    /// Removes M2 Slot from a Motherboard
    /// </summary>
    /// <param name="commandParametr">M2 Slot to remove</param>
    public void PerformRemoveM2Slot(object? commandParametr)
    {
        if (commandParametr is M2Slot m2Slot)
        {
            Motherboard.M2Slots.Remove(m2Slot);
            RemovedM2Slots.Add(m2Slot);
        }
    }

    private RelayCommand _addM2Slot;

    /// <summary>
    /// Creates new NewMotherboardViewmodel with given Motherboard
    /// </summary>
    /// <param name="motherboard">Motherboard that is beging added</param>
    public NewMotherboardViewmodel(Motherboard motherboard)
    {
        Motherboard = motherboard;
    }

    /// <summary>
    /// Command to add M2 Slot to a Motherboard
    /// </summary>
    public ICommand AddM2Slot => _addM2Slot ??= new RelayCommand(PerformAddM2Slot);

    /// <summary>
    /// Adds new M2 Slot to a Motherboard
    /// </summary>
    /// <param name="commandParameter">Not used</param>
    private void PerformAddM2Slot(object? commandParameter)
    {
        if (Motherboard.M2Slots.Count <= 8)
            Motherboard.M2Slots.Add(new M2Slot() { M2Interface = M2Interface.Nvme, M2Size = M2Size._2260 });
    }
}

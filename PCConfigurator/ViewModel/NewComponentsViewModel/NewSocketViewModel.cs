using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

internal class NewSocketViewModel(Socket socket)
{
    public Socket Socket { get; } = socket;

    private RelayCommand _removeChipset;
    public ICommand RemoveChipset => _removeChipset ??= new RelayCommand(PerformRemoveChipset);
    public void PerformRemoveChipset(object? commandParametr)
    {
        if (commandParametr is Chipset chipset)
        {
            Socket.Chipsets.Remove(chipset);
        }
    }

    private RelayCommand _addChipset;
    public ICommand AddChipset => _addChipset ??= new RelayCommand(PerformAddChipset);
    public void PerformAddChipset(object? commandParametr)
    {
        Socket.Chipsets.Add(new Chipset());
    }
}

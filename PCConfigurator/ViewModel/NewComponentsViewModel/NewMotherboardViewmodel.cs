﻿using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using PCConfigurator.Model.Components.M2;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

internal class NewMotherboardViewmodel(Motherboard motherboard) : BaseViewModel
{
    public Motherboard Motherboard { get; } = motherboard;

    public List<M2Slot> RemovedM2Slots { get; } = [];

    private RelayCommand _removeM2Slot;
    public ICommand RemoveM2Slot => _removeM2Slot ??= new RelayCommand(PerformRemoveM2Slot);
    public void PerformRemoveM2Slot(object? commandParametr)
    {
        if (commandParametr is M2Slot m2Slot)
        {
            Motherboard.M2Slots.Remove(m2Slot);
            RemovedM2Slots.Add(m2Slot);
        }
    }

    private RelayCommand _addM2Slot;
    public ICommand AddM2Slot => _addM2Slot ??= new RelayCommand(PerformAddM2Slot);
    private void PerformAddM2Slot(object? commandParameter)
    {
        if (Motherboard.M2Slots.Count <= 8)
            Motherboard.M2Slots.Add(new M2Slot() { M2Interface = M2Interface.Nvme, M2Size = M2Size._2260 });
    }
}

using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.NewComponentsViewModel
{
    internal class NewMotherboardViewmodel(Motherboard motherboard)
    {
        public Motherboard Motherboard { get; } = motherboard;


        private RelayCommand _removeM2Slot;

        [NotMapped]
        public ICommand RemoveM2Slot => _removeM2Slot ??= new RelayCommand(PerformRemoveM2Slot);

        public void PerformRemoveM2Slot(object? commandParametr)
        {
            if (commandParametr is M2Slot m2Slot)
            {
                Motherboard.M2Slots.Remove(m2Slot);
            }
        }

        private RelayCommand _addM2Slot;
        public ICommand AddM2Slot => _addM2Slot ??= new RelayCommand(PerformAddM2Slot);

        private void PerformAddM2Slot(object? commandParameter)
        {
            if (Motherboard.M2Slots.Count <= 8)
                Motherboard.M2Slots.Add(new M2Slot());
        }
    }
}

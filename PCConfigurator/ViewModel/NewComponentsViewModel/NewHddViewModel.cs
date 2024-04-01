using PCConfigurator.Model.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

internal class NewHddViewModel(Hdd hdd) : BaseViewModel
{
    public Hdd Hdd { get; } = hdd;

    private bool _tb = hdd.Capacity > 1024;

    public bool Tb
    {
        get => _tb;

        set
        {
            _tb = value;
            OnPropertyChanged(nameof(Capacity));
        }
    }

    public int Capacity
    {
        get
        {
            if (Tb)
            {
                int result = Hdd.Capacity / 1000;
                return result > 0 ? result : 1;
            }
            else
                return Hdd.Capacity;
        }
        set
        {
            if (Tb)
                Hdd.Capacity = value * 1000;
            else
                Hdd.Capacity = value;

            OnPropertyChanged(nameof(Capacity));
        }
    }
}

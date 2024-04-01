using PCConfigurator.Model.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

internal class NewSsdViewModel(Ssd ssd) : BaseViewModel
{
    public Ssd Ssd { get; } = ssd;

    private bool _tb = ssd.Capacity > 1024;

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
                int result = Ssd.Capacity / 1000;
                return result > 0 ? result : 1;
            }
            else
                return Ssd.Capacity;
        }
        set
        {
            if (Tb)
                Ssd.Capacity = value * 1000;
            else
                Ssd.Capacity = value;

            OnPropertyChanged(nameof(Capacity));
        }
    }
}

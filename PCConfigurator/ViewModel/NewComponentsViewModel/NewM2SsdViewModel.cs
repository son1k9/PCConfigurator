using PCConfigurator.Model.Components;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

internal class NewM2SsdViewModel(M2Ssd m2ssd) : BaseViewModel
{
    public M2Ssd M2Ssd { get; } = m2ssd;

    private bool _tb = m2ssd.Capacity > 1024;

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
                int result = M2Ssd.Capacity / 1000;
                return result > 0 ? result : 1;
            }
            else
                return M2Ssd.Capacity;
        }
        set
        {
            if (Tb)
                M2Ssd.Capacity = value * 1000;
            else
                M2Ssd.Capacity = value;

            OnPropertyChanged(nameof(Capacity));
        }
    }
}

using PCConfigurator.Model.Components;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

/// <summary>
/// ViewModel for addition of a new M2 SSD
/// </summary>
public class NewM2SsdViewModel : BaseViewModel
{
    /// <summary>
    /// M2 SSD that is being added
    /// </summary>
    public M2Ssd M2Ssd { get; }

    private bool _tb;

    /// <summary>
    /// Creates new NewM2SSDViewmodel with given M2 SSD
    /// </summary>
    /// <param name="m2ssd">M2 SSD that is beging added</param>
    public NewM2SsdViewModel(M2Ssd m2ssd)
    {
        M2Ssd = m2ssd;
        _tb = m2ssd.Capacity > 1024;
    }

    /// <summary>
    /// Indicates if current Capacity is in Terabytes
    /// </summary>
    public bool Tb
    {
        get => _tb;

        set
        {
            _tb = value;
            OnPropertyChanged(nameof(Capacity));
        }
    }

    /// <summary>
    /// Capacity of M2 SSD
    /// </summary>
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

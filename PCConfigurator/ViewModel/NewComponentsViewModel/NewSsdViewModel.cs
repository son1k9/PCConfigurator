using PCConfigurator.Model.Components;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

/// <summary>
/// ViewModel for addition of a new SSD
/// </summary>
public class NewSsdViewModel : BaseViewModel
{
    /// <summary>
    /// Ssd that is being added
    /// </summary>
    public Ssd Ssd { get; }

    private bool _tb;

    /// <summary>
    /// Creates new NewSsdViewmodel with given Ssd
    /// </summary>
    /// <param name="ssd">Ssd that is beging added</param>
    public NewSsdViewModel(Ssd ssd)
    {
        Ssd = ssd;
        _tb = ssd.Capacity > 1024;
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
    /// Capacity of Ssd
    /// </summary>
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

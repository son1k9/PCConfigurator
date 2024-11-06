using PCConfigurator.Model.Components;

namespace PCConfigurator.ViewModel.NewComponentsViewModel;

/// <summary>
/// ViewModel for addition of a new HDD
/// </summary>
public class NewHddViewModel : BaseViewModel
{
    /// <summary>
    /// HDD that is being added
    /// </summary>
    public Hdd Hdd { get; }

    private bool _tb;

    /// <summary>
    /// Creates new NewHDDViewmodel with given HDD
    /// </summary>
    /// <param name="hdd">HDD that is beging added</param>
    public NewHddViewModel(Hdd hdd)
    {
        Hdd = hdd;
        _tb = hdd.Capacity > 1024;
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
    /// Capacity of HDD
    /// </summary>
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

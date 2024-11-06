using PCConfigurator.Commands;
using PCConfigurator.ViewModel.ComponentsViewModels;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

/// <summary>
/// ViewModel to manage navigation between ComponentsViewModels
/// </summary>
public class ComponentsViewModel : BaseViewModel
{
    private ComponentViewModel _currentViewModel = new MotherboardViewModel();

    /// <summary>
    /// Current chosen ComponentViewModel
    /// </summary>
    public ComponentViewModel CurrentViewModel 
    { 
        get => _currentViewModel; 
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    /// <summary>
    /// Creates new ComponentsViewModel with all navigation commands initialized
    /// </summary>
    public ComponentsViewModel()
    {
        NavigateMotherboardCommand = new NavigateCommand<MotherboardViewModel>(this);
        NavigateCpuCommand = new NavigateCommand<CpuViewModel>(this);
        NavigateCoolerCommand = new NavigateCommand<CoolerViewModel>(this);
        NavigateRamCommand = new NavigateCommand<RamViewModel>(this);
        NavigatePowerSupplyCommand = new NavigateCommand<PowerSupplyViewModel>(this);
        NavigateGpuCommand = new NavigateCommand<GpuViewModel>(this);
        NavigateSsdCommand = new NavigateCommand<SsdViewModel>(this);
        NavigateHddCommand = new NavigateCommand<HddViewModel>(this);
        NavigateM2SsdCommand = new NavigateCommand<M2SsdViewModel>(this);
        NavigateSocketCommand = new NavigateCommand<SocketViewModel>(this);
    }   

    /// <summary>
    /// Command to navigate to motherboard's ViewModel 
    /// </summary>
    public ICommand NavigateMotherboardCommand { get; }

    /// <summary>
    /// Command to navigate to CPU's ViewModel 
    /// </summary>
    public ICommand NavigateCpuCommand { get; }

    /// <summary>
    /// Command to navigate to cooler's ViewModel 
    /// </summary>
    public ICommand NavigateCoolerCommand { get; }

    /// <summary>
    /// Command to navigate to RAM's ViewModel 
    /// </summary>
    public ICommand NavigateRamCommand { get; }

    /// <summary>
    /// Command to navigate to power supply's ViewModel 
    /// </summary>
    public ICommand NavigatePowerSupplyCommand { get; }

    /// <summary>
    /// Command to navigate to GPU's ViewModel 
    /// </summary>
    public ICommand NavigateGpuCommand { get; }

    /// <summary>
    /// Command to navigate to SSD's ViewModel 
    /// </summary>
    public ICommand NavigateSsdCommand { get; }

    /// <summary>
    /// Command to navigate to HDD's ViewModel 
    /// </summary>
    public ICommand NavigateHddCommand { get; }

    /// <summary>
    /// Command to navigate to M2 SSD's ViewModel 
    /// </summary>
    public ICommand NavigateM2SsdCommand { get; }

    /// <summary>
    /// Command to navigate to socket's ViewModel 
    /// </summary>
    public ICommand NavigateSocketCommand { get; }
}

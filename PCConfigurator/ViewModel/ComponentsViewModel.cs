using PCConfigurator.Commands;
using PCConfigurator.ViewModel.ComponentsViewModels;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

internal class ComponentsViewModel : BaseViewModel
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

    public ICommand NavigateMotherboardCommand { get; }
    public ICommand NavigateCpuCommand { get; }
    public ICommand NavigateCoolerCommand { get; }
    public ICommand NavigateRamCommand { get; }
    public ICommand NavigatePowerSupplyCommand { get; }
    public ICommand NavigateGpuCommand { get; }
    public ICommand NavigateSsdCommand { get; }
    public ICommand NavigateHddCommand { get; }
    public ICommand NavigateM2SsdCommand { get; }
    public ICommand NavigateSocketCommand { get; }
}

using PCConfigurator.Commands;
using PCConfigurator.Stores;
using PCConfigurator.ViewModel.ComponentsViewModels;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

internal class ComponentsViewModel : BaseViewModel
{
    private readonly NavigationStorage _navigationStorage;

    public BaseViewModel CurrentViewModel => _navigationStorage.CurrentViewModel;

    public ComponentsViewModel()
    {
        _navigationStorage = new NavigationStorage(new MotherboardViewModel());
        _navigationStorage.ViewModelChanged += OnViewModelChanged;
        NavigateMotherboardCommand = new NavigateCommand<MotherboardViewModel>(_navigationStorage);
        NavigateCpuCommand = new NavigateCommand<CpuViewModel>(_navigationStorage);
        NavigateCoolerCommand = new NavigateCommand<CoolerViewModel>(_navigationStorage);
        NavigateRamCommand = new NavigateCommand<RamViewModel>(_navigationStorage);
        NavigatePowerSupplyCommand = new NavigateCommand<PowerSupplyViewModel>(_navigationStorage);
        NavigateGpuCommand = new NavigateCommand<GpuViewModel>(_navigationStorage);
        NavigateSsdCommand = new NavigateCommand<SsdViewModel>(_navigationStorage);
        NavigateHddCommand = new NavigateCommand<HddViewModel>(_navigationStorage);
        NavigateM2SsdCommand = new NavigateCommand<M2SsdViewModel>(_navigationStorage);
        NavigateSocketCommand = new NavigateCommand<SocketViewModel>(_navigationStorage);
    }

    private void OnViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
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

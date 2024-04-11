using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

internal class ConfigurationViewModel : BaseViewModel
{
    private Configuration _configuration;
    public Configuration Configuration { get => _configuration; }

    public event EventHandler<object>? ArrayChanged;

    private void OnArrayChanged(object array)
    {
        ArrayChanged?.Invoke(this, array);
    }

    private Action _saveChanges;

    public ConfigurationViewModel(Configuration configuration, Action saveChanges)
    {
        _configuration = configuration;
        _saveChanges = saveChanges;
        name = _configuration.Name;
        motherboard = _configuration.Motherboard;
        cpu = _configuration.Cpu;
        cooler = _configuration.Cooler;
        powerSupply = _configuration.PowerSupply;


        _rams = new Ram[configuration.Motherboard.RamSlots];
        int i = 0;
        foreach (ConfigurationRam configurationRam in configuration.ConfigurationRams) 
            _rams[i++] = configurationRam.Ram;


        _gpus = new Gpu[configuration.Motherboard.PCIex16Slots];
        i = 0;
        foreach (ConfigurationGpu configurationGpu in configuration.ConfigurationGpus)
            _gpus[i++] = configurationGpu.Gpu;


        _m2Slots = new M2Slot[configuration.Motherboard.M2Slots.Count];
        _m2Ssds = new M2Ssd[configuration.Motherboard.M2Slots.Count];
        i = 0;
        foreach (ConfigurationM2Ssd configurationM2Ssd in configuration.ConfigurationM2Ssds) 
        {
            _m2Ssds[i] = configurationM2Ssd.M2Ssd;
            _m2Slots[i] = configurationM2Ssd.M2Slot;
            i++;
        }
        var query = from n in configuration.Motherboard.M2Slots
                    where !_m2Slots.Contains(n)
                    select n;
        foreach (M2Slot m2Slot in query)
        {
            _m2Ssds[i] = null;
            _m2Slots[i] = m2Slot;
            i++;
        }


        _ssdsAndHdds = new Model.Components.Component[configuration.Motherboard.Sata3Ports];
        i = 0;
        foreach(ConfigurationSsd configurationSsd in configuration.ConfigurationSsds)
            _ssdsAndHdds[i++] = configurationSsd.Ssd;
        foreach(ConfigurationHdd configurationHdd in configuration.ConfigurationHdds)
            _ssdsAndHdds[i++] = configurationHdd.Hdd;

        PropertyChanged += AfterMotherboardReset;
    }

    private string name;
    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    private Motherboard? motherboard;
    public Motherboard? Motherboard
    {
        get => motherboard;
        set
        {
            motherboard = value;
            OnPropertyChanged(nameof(Motherboard));
        }
    }

    private Cpu? cpu;
    public Cpu? Cpu 
    { 
        get => cpu;
        set 
        { 
            cpu = value;
            OnPropertyChanged(nameof(Cpu));
        }
    }

    private Cooler? cooler;
    public Cooler? Cooler 
    { 
        get => cooler;
        set 
        {
            cooler = value;
            OnPropertyChanged(nameof(Cooler));
        }
    }

    private PowerSupply? powerSupply;
    public PowerSupply? PowerSupply 
    {
        get => powerSupply;
        set 
        { 
            powerSupply = value;
            OnPropertyChanged(nameof(PowerSupply));
        }
    }

    private Ram?[] _rams;
    public Ram?[] Rams 
    { 
        get => _rams;
        set
        {
            _rams = value;
            OnPropertyChanged(nameof(Rams));
        }
    }

    private void SetRam(int index, Ram? ram)
    {
        if (index >= 0 && index < Rams.Length)
        {
            Rams[index] = ram;
            OnArrayChanged(Rams);
        }
    }

    private Gpu?[] _gpus;
    public Gpu?[] Gpus
    {
        get => _gpus;
        set
        {
            _gpus = value;
            OnPropertyChanged(nameof(Gpus));
        }
    }

    private void SetGpu(int index, Gpu? gpu)
    {
        if (index >= 0 && index < Gpus.Length)
        {
            Gpus[index] = gpu;
            OnArrayChanged(Gpus);
        }
    }

    private M2Ssd?[] _m2Ssds;
    public M2Ssd?[] M2Ssds
    {
        get => _m2Ssds;
        set
        {
            _m2Ssds = value;
            OnPropertyChanged(nameof(M2Ssds));
        }
    }

    private void SetM2Ssd(int index, M2Ssd? m2Ssd)
    {
        if (index >= 0 && index < M2Ssds.Length)
        {
            M2Ssds[index] = m2Ssd;
            OnArrayChanged(M2Ssds);
        }
    }

    private M2Slot?[] _m2Slots;
    public M2Slot?[] M2Slots
    {
        get => _m2Slots;
        set
        {
            _m2Slots = value;
            OnPropertyChanged(nameof(M2Slots));
        }
    }

    private Model.Components.Component?[] _ssdsAndHdds;
    public Model.Components.Component?[] SsdAndHdds
    {
        get => _ssdsAndHdds;
        set
        {
            _ssdsAndHdds = value;
            OnPropertyChanged(nameof(SsdAndHdds));
        }
    }

    private void SetSata(int index, Model.Components.Component? component)
    {
        if (index >= 0 && index < SsdAndHdds.Length)
        {
            SsdAndHdds[index] = component;
            OnArrayChanged(SsdAndHdds);
        }
    }

    private RelayCommand? _save;
    public ICommand Save => _save ??= new RelayCommand(PerformSave);
    private void PerformSave(object? commandParameter)
    {
        _configuration.Name = Name;
        _saveChanges.Invoke();
    }

    private RelayCommand? _resetMotherboard;
    public ICommand ResetMotherboard => _resetMotherboard ??= new RelayCommand(PerformResetMotherboard);
    private void PerformResetMotherboard(object? commandParameter)
    {
        Motherboard = null;
    }

    private RelayCommand? _resetCpu;
    public ICommand ResetCpu => _resetCpu ??= new RelayCommand(PerformResetCpu);
    private void PerformResetCpu(object? commandParameter)
    {
        Cpu = null;
    }

    private RelayCommand? _resetCooler;
    public ICommand ResetCooler => _resetCooler ??= new RelayCommand(PerformResetCooler);
    private void PerformResetCooler(object? commandParameter)
    {
        Cooler = null;
    }

    private RelayCommand? _resetPowerSupply;
    public ICommand ResetPowerSupply => _resetPowerSupply ??= new RelayCommand(PerformResetPowerSupply);
    private void PerformResetPowerSupply(object? commandParameter)
    {
        PowerSupply = null;
    }

    private RelayCommand? _resetRam;
    public ICommand ResetRam => _resetRam ??= new RelayCommand(PerformResetRam);
    private void PerformResetRam(object? commandParameter)
    {
        if (commandParameter is int index)
        {
            SetRam(index, null);
        }
    }

    private RelayCommand? _resetGpu;
    public ICommand ResetGpu => _resetGpu ??= new RelayCommand(PerformResetGpu);
    private void PerformResetGpu(object? commandParameter)
    {
        if (commandParameter is int index)
        {
            SetGpu(index, null);
        }
    }

    private RelayCommand? _resetM2Ssd;
    public ICommand ResetM2Ssd => _resetM2Ssd ??= new RelayCommand(PerformResetM2Ssd);
    private void PerformResetM2Ssd(object? commandParameter)
    {
        if (commandParameter is int index)
        {
            SetM2Ssd(index, null);
        }
    }

    private RelayCommand? _resetSata;
    public ICommand ResetSata => _resetSata ??= new RelayCommand(PerformResetSata);
    private void PerformResetSata(object? commandParameter)
    {
        if (commandParameter is int index)
        {
            SetSata(index, null);
        }
    }

    private void AfterMotherboardReset(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Motherboard")
        {
            Rams = new Ram[(Motherboard?.RamSlots).GetValueOrDefault()];
            Gpus = new Gpu[(Motherboard?.PCIex16Slots).GetValueOrDefault()];
            M2Slots = new M2Slot[(Motherboard?.M2Slots.Count).GetValueOrDefault()];
            M2Ssds = new M2Ssd[(Motherboard?.M2Slots.Count).GetValueOrDefault()];

            int i = 0;
            if (Motherboard is not null)
            {
                foreach (var M2Slot in Motherboard.M2Slots)
                    M2Slots[i++] = M2Slot;
            }

            SsdAndHdds = new Model.Components.Component[(Motherboard?.Sata3Ports).GetValueOrDefault()];
        }
    }
}

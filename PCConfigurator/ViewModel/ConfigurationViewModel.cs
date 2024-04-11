using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using System.Windows;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

internal class ConfigurationViewModel : BaseViewModel
{
    private Configuration _configuration;
    public Configuration Configuration { get => _configuration; }

    private Action _saveChanges;

    public ConfigurationViewModel(Configuration configuration, Action saveChanges)
    {
        _configuration = configuration;
        _saveChanges = saveChanges;
        Name = _configuration.Name;
        Motherboard = _configuration.Motherboard;
        Cpu = _configuration.Cpu;
        Cooler = _configuration.Cooler;
        PowerSupply = _configuration.PowerSupply;


        _rams = new Ram[configuration.Motherboard.RamSlots];
        int i = 0;
        foreach (ConfigurationRam configurationRam in configuration.ConfigurationRams) 
            _rams[i++] = configurationRam.Ram;
        for (; i < _rams.Length; i++)
            _rams[i] = new Ram();


        _gpus = new Gpu[configuration.Motherboard.PCIex16Slots];
        i = 0;
        foreach (ConfigurationGpu configurationGpu in configuration.ConfigurationGpus)
            _gpus[i++] = configurationGpu.Gpu;
        for (; i < _gpus.Length; i++)
            _gpus[i] = new Gpu();


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
            _m2Ssds[i] = new M2Ssd();
            _m2Slots[i] = m2Slot;
            i++;
        }


        _ssdsAndHdds = new Component[configuration.Motherboard.Sata3Ports];
        i = 0;
        foreach(ConfigurationSsd configurationSsd in configuration.ConfigurationSsds)
            _ssdsAndHdds[i++] = configurationSsd.Ssd;
        foreach(ConfigurationHdd configurationHdd in configuration.ConfigurationHdds)
            _ssdsAndHdds[i++] = configurationHdd.Hdd;
        for (; i < _ssdsAndHdds.Length; i++)
            _ssdsAndHdds[i] = new Hdd();
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

    private Motherboard motherboard;
    public Motherboard Motherboard
    {
        get => motherboard;
        set
        {
            motherboard = value;
            OnPropertyChanged(nameof(Motherboard));
        }
    }

    private Cpu cpu;
    public Cpu Cpu 
    { 
        get => cpu;
        set 
        { 
            cpu = value;
            OnPropertyChanged(nameof(Cpu));
        }
    }

    public Cooler Cooler { get; set; }

    public PowerSupply PowerSupply { get; set; }

    private Ram[] _rams;
    public Ram[] Rams 
    { 
        get => _rams;
        set
        {
            _rams = value;
            OnPropertyChanged(nameof(Rams));
        }
    }

    private Gpu[] _gpus;
    public Gpu[] Gpus
    {
        get => _gpus;
        set
        {
            _gpus = value;
            OnPropertyChanged(nameof(Gpus));
        }
    }

    private M2Ssd[] _m2Ssds;
    public M2Ssd[] M2Ssds
    {
        get => _m2Ssds;
        set
        {
            _m2Ssds = value;
            OnPropertyChanged(nameof(M2Ssds));
        }
    }

    private M2Slot[] _m2Slots;
    public M2Slot[] M2Slots
    {
        get => _m2Slots;
        set
        {
            _m2Slots = value;
            OnPropertyChanged(nameof(M2Slots));
        }
    }


    private Component[] _ssdsAndHdds;
    public Component[] SsdAndHdds
    {
        get => _ssdsAndHdds;
        set
        {
            _ssdsAndHdds = value;
            OnPropertyChanged(nameof(SsdAndHdds));
        }
    }

    private RelayCommand _save;

    public ICommand Save => _save ??= new RelayCommand(PerformSave);
    private void PerformSave(object? commandParameter)
    {
        _configuration.Name = Name;
        _saveChanges.Invoke();
    }
}

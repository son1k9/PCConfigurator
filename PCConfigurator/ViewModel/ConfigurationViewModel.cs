using Microsoft.EntityFrameworkCore;
using PCConfigurator.Commands;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.View.ComponentСhoice;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

internal class ConfigurationViewModel : BaseViewModel
{
    private readonly Configuration _configuration;
    public Configuration Configuration { get => _configuration; }

    public event EventHandler? ConfigurationSaved;

    private void OnSave()
    {
        ConfigurationSaved?.Invoke(this, EventArgs.Empty);
    }

    private string[] errors;
    public string[] Errors
    {
        get => errors;
        set
        {
            errors = value;
            OnPropertyChanged(nameof(Errors));
        }
    }

    public event EventHandler<object>? ArrayChanged;

    private bool _changes = false;
    public bool Changes
    {
        get => _changes;
        set
        {
            _changes = value;
            OnPropertyChanged(nameof(Changes));
        }
    }

    private bool _add;

    private void OnArrayChanged(object array)
    {
        ArrayChanged?.Invoke(this, array);
    }

    private readonly ApplicationContext dbContext;

    public ConfigurationViewModel(Configuration configuration, ApplicationContext context, bool add = false)
    {
        _add = add;
        if (_add)
            Changes = true;
        _configuration = configuration;
        dbContext = context;
        name = _configuration.Name;
        motherboard = _configuration.Motherboard;
        cpu = _configuration.Cpu;
        cooler = _configuration.Cooler;
        powerSupply = _configuration.PowerSupply;
        errors = configuration.CheckCompatibility();

        if (configuration.Motherboard != null)
        {
            _rams = new Ram[configuration.Motherboard.RamSlots];
            int i = 0;
            foreach (ConfigurationRam configurationRam in configuration.ConfigurationRams)
                _rams[i++] = configurationRam.Ram;


            _gpus = new Gpu[configuration.Motherboard.PCIex16Slots];
            i = 0;
            foreach (ConfigurationGpu configurationGpu in configuration.ConfigurationGpus)
                _gpus[i++] = configurationGpu.Gpu;


            M2Slot[] _m2Slots = new M2Slot[configuration.Motherboard.M2Slots.Count];
            _m2Ssds = new M2SlotWithSsd[configuration.Motherboard.M2Slots.Count];
            i = 0;

            foreach (ConfigurationM2Ssd configurationM2Ssd in configuration.ConfigurationM2Ssds)
            {
                _m2Ssds[i] = new M2SlotWithSsd(configurationM2Ssd.M2Slot, configurationM2Ssd.M2Ssd);
                _m2Slots[i] = configurationM2Ssd.M2Slot;
                i++;
            }

            var query = from n in configuration.Motherboard.M2Slots
                        where !_m2Slots.Contains(n)
                        select n;

            foreach (M2Slot m2Slot in query)
            {
                _m2Ssds[i++] = new M2SlotWithSsd(m2Slot, null);
            }


            _ssdsAndHdds = new Model.Components.Component[configuration.Motherboard.Sata3Ports];
            i = 0;
            foreach (ConfigurationSsd configurationSsd in configuration.ConfigurationSsds)
                _ssdsAndHdds[i++] = configurationSsd.Ssd;
            foreach (ConfigurationHdd configurationHdd in configuration.ConfigurationHdds)
                _ssdsAndHdds[i++] = configurationHdd.Hdd;
        }

        PropertyChanged += AfterMotherboardChange;
    }

    private string name;
    public string Name
    {
        get => name;
        set
        {
            name = value;
            Changes = true;
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
            Changes = true;
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
            Changes = true;
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
            Changes = true;
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
            Changes = true;
            OnPropertyChanged(nameof(PowerSupply));
        }
    }

    private Ram?[]? _rams;
    public Ram?[]? Rams
    {
        get => _rams;
        set
        {
            _rams = value;
            Changes = true;
            OnPropertyChanged(nameof(Rams));
        }
    }

    private void SetRam(int index, Ram? ram)
    {
        if (index >= 0 && index < Rams?.Length)
        {
            Rams[index] = ram;
            Changes = true;
            OnArrayChanged(Rams);
        }
    }

    private Gpu?[]? _gpus;
    public Gpu?[]? Gpus
    {
        get => _gpus;
        set
        {
            _gpus = value;
            Changes = true;
            OnPropertyChanged(nameof(Gpus));
        }
    }

    private void SetGpu(int index, Gpu? gpu)
    {
        if (index >= 0 && index < Gpus?.Length)
        {
            Gpus[index] = gpu;
            Changes = true;
            OnArrayChanged(Gpus);
        }
    }

    private void SetM2Ssd(int index, M2Ssd? m2Ssd)
    {
        if (index >= 0 && index < M2Ssds?.Length)
        {
            if (M2Ssds[index] is not null)
            {
                M2Ssds[index].M2Ssd = m2Ssd;
                Changes = true;
                OnArrayChanged(M2Ssds);
            }
        }
    }

    public class M2SlotWithSsd(M2Slot m2slot, M2Ssd? m2ssd)
    {
        public M2Slot M2Slot { get; set; } = m2slot;
        public M2Ssd? M2Ssd { get; set; } = m2ssd;
    }

    private M2SlotWithSsd[]? _m2Ssds;
    public M2SlotWithSsd[]? M2Ssds
    {
        get => _m2Ssds;
        set
        {
            _m2Ssds = value;
            Changes = true;
            OnPropertyChanged(nameof(M2Ssds));
        }
    }

    private Model.Components.Component?[]? _ssdsAndHdds;
    public Model.Components.Component?[]? SsdAndHdds
    {
        get => _ssdsAndHdds;
        set
        {
            _ssdsAndHdds = value;
            Changes = true;
            OnPropertyChanged(nameof(SsdAndHdds));
        }
    }

    private void SetSata(int index, Model.Components.Component? component)
    {
        if (index >= 0 && index < SsdAndHdds?.Length)
        {
            SsdAndHdds[index] = component;
            Changes = true;
            OnArrayChanged(SsdAndHdds);
        }
    }

    private RelayCommand? _save;
    public ICommand Save => _save ??= new RelayCommand(PerformSave, (obj) => Changes);
    private void PerformSave(object? commandParameter)
    {
        if (Name.Length == 0)
        {
            MessageBox.Show("Неудалось сохранить конфигурацию, так как не было указано название.", "Ошибка");
            return;
        }

        _configuration.Name = Name;
        _configuration.Motherboard = Motherboard;
        _configuration.Cpu = Cpu;
        _configuration.Cooler = Cooler;
        _configuration.PowerSupply = PowerSupply;

        //RAM
        List<ConfigurationRam> rams = [];
        for (int i = 0; i < Rams?.Length; i++)
        {
            if (Rams[i] is Ram ram)
                rams.Add(new ConfigurationRam { Configuration = _configuration, Ram = ram });
        }
        _configuration.ConfigurationRams = rams;

        //GPU
        List<ConfigurationGpu> gpus = [];
        for (int i = 0; i < Gpus?.Length; i++)
        {
            if (Gpus[i] is Gpu gpu)
                gpus.Add(new ConfigurationGpu { Configuration = _configuration, Gpu = gpu });
        }
        _configuration.ConfigurationGpus = gpus;

        //M2SSD
        List<ConfigurationM2Ssd> m2ssds = [];
        for (int i = 0; i < M2Ssds?.Length; i++)
        {
            if (M2Ssds[i].M2Ssd is M2Ssd m2ssd)
                m2ssds.Add(new ConfigurationM2Ssd { Configuration = _configuration, M2Slot = M2Ssds[i].M2Slot, M2Ssd = m2ssd });
        }
        _configuration.ConfigurationM2Ssds = m2ssds;

        //Sata
        List<ConfigurationHdd> hdds = [];
        List<ConfigurationSsd> ssds = [];
        for (int i = 0; i < SsdAndHdds?.Length; i++)
        {
            if (SsdAndHdds[i] is Hdd hdd)
                hdds.Add(new ConfigurationHdd { Configuration = _configuration, Hdd = hdd });
            else if (SsdAndHdds[i] is Ssd ssd)
                ssds.Add(new ConfigurationSsd { Configuration = _configuration, Ssd = ssd });
        }
        _configuration.ConfigurationHdds = hdds;
        _configuration.ConfigurationSsds = ssds;

        if (_add)
            dbContext.Configuration.Add(_configuration);

        dbContext.SaveChanges();
        Changes = false;
        OnSave();
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

    private void AfterMotherboardChange(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Motherboard")
        {
            Rams = new Ram[(Motherboard?.RamSlots).GetValueOrDefault()];
            Gpus = new Gpu[(Motherboard?.PCIex16Slots).GetValueOrDefault()];

            M2SlotWithSsd[] m2Ssds = new M2SlotWithSsd[(Motherboard?.M2Slots.Count).GetValueOrDefault()];
            int i = 0;
            if (Motherboard is not null)
            {
                foreach (var M2Slot in Motherboard.M2Slots)
                    m2Ssds[i++] = new M2SlotWithSsd(M2Slot, null);
            }
            M2Ssds = m2Ssds;
            SsdAndHdds = new Model.Components.Component[(Motherboard?.Sata3Ports).GetValueOrDefault()];
        }
    }

    private RelayCommand? _changeMotherboard;
    public ICommand ChangeMotherboard => _changeMotherboard ??= new RelayCommand(PerformChangeMotherboard);
    private void PerformChangeMotherboard(object? commandParameter)
    {
        MotherboardsListUserControl list = new MotherboardsListUserControl();
        dbContext.Motherboard.Load();
        list.dataGrid.ItemsSource = dbContext.Motherboard.Local.ToList();
        ComponentChoiceWindow window = new ComponentChoiceWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow
        };
        window.ComponentsList.Content = list;
        if (window.ShowDialog() == true)
        {
            if (list.dataGrid.SelectedItem is Motherboard motherboard && Motherboard != motherboard)
                Motherboard = motherboard;
        }
    }

    private RelayCommand? _changeCpu;
    public ICommand ChangeCpu => _changeCpu ??= new RelayCommand(PerformChangeCpu);
    private void PerformChangeCpu(object? commandParameter)
    {
        CpuListUserControl list = new CpuListUserControl();
        dbContext.Cpu.Load();
        list.dataGrid.ItemsSource = dbContext.Cpu.Local.ToList();
        ComponentChoiceWindow window = new ComponentChoiceWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow
        };
        window.ComponentsList.Content = list;
        if (window.ShowDialog() == true)
        {
            if (list.dataGrid.SelectedItem is Cpu cpu)
                Cpu = cpu;
        }
    }

    private RelayCommand? _changeCooler;
    public ICommand ChangeCooler => _changeCooler ??= new RelayCommand(PerformChangeCooler);
    private void PerformChangeCooler(object? commandParameter)
    {
        CoolerListUserControl list = new CoolerListUserControl();
        dbContext.Cooler.Load();
        list.dataGrid.ItemsSource = dbContext.Cooler.Local.ToList();
        ComponentChoiceWindow window = new ComponentChoiceWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow
        };
        window.ComponentsList.Content = list;
        if (window.ShowDialog() == true)
        {
            if (list.dataGrid.SelectedItem is Cooler cooler)
                Cooler = cooler;
        }
    }

    private RelayCommand? _changePowerSupply;
    public ICommand ChangePowerSupply => _changePowerSupply ??= new RelayCommand(PerformChangePowerSupply);
    private void PerformChangePowerSupply(object? commandParameter)
    {
        PowerSupplyListUserControl list = new PowerSupplyListUserControl();
        dbContext.PowerSupply.Load();
        list.dataGrid.ItemsSource = dbContext.PowerSupply.Local.ToList();
        ComponentChoiceWindow window = new ComponentChoiceWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow
        };
        window.ComponentsList.Content = list;
        if (window.ShowDialog() == true)
        {
            if (list.dataGrid.SelectedItem is PowerSupply powerSupply)
                PowerSupply = powerSupply;
        }
    }

    private RelayCommand? _changeRam;
    public ICommand ChangeRam => _changeRam ??= new RelayCommand(PerformChangeRam);
    private void PerformChangeRam(object? commandParameter)
    {
        RamListUserControl list = new RamListUserControl();
        dbContext.Ram.Load();
        list.dataGrid.ItemsSource = dbContext.Ram.Local.ToList();
        ComponentChoiceWindow window = new ComponentChoiceWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow
        };
        window.ComponentsList.Content = list;
        if (window.ShowDialog() == true)
        {
            if (list.dataGrid.SelectedItem is Ram ram && commandParameter is int index)
                SetRam(index, ram);
        }
    }

    private RelayCommand? _changeGpu;
    public ICommand ChangeGpu => _changeGpu ??= new RelayCommand(PerformChangeGpu);
    private void PerformChangeGpu(object? commandParameter)
    {
        GpuListUserControl list = new GpuListUserControl();
        dbContext.Gpu.Load();
        list.dataGrid.ItemsSource = dbContext.Gpu.Local.ToList();
        ComponentChoiceWindow window = new ComponentChoiceWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow
        };
        window.ComponentsList.Content = list;
        if (window.ShowDialog() == true)
        {
            if (list.dataGrid.SelectedItem is Gpu gpu && commandParameter is int index)
                SetGpu(index, gpu);
        }
    }

    private RelayCommand? _changeM2Ssd;
    public ICommand ChangeM2Ssd => _changeM2Ssd ??= new RelayCommand(PerformChangeM2Ssd);
    private void PerformChangeM2Ssd(object? commandParameter)
    {
        M2SsdListUserControl list = new M2SsdListUserControl();
        dbContext.M2Ssd.Load();
        list.dataGrid.ItemsSource = dbContext.M2Ssd.Local.ToList();
        ComponentChoiceWindow window = new ComponentChoiceWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow
        };
        window.ComponentsList.Content = list;
        if (window.ShowDialog() == true)
        {
            if (list.dataGrid.SelectedItem is M2Ssd m2ssd && commandParameter is int index)
                SetM2Ssd(index, m2ssd);
        }
    }

    private RelayCommand? _changeSata;

    public ICommand ChangeSata => _changeSata ??= new RelayCommand(PerformChangeSata);
    private void PerformChangeSata(object? commandParameter)
    {
        SataComponentChoiceWindow window = new SataComponentChoiceWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow
        };
        if (window.ShowDialog() == true)
        {
            if (window.Clicked == SataComponentChoiceWindow.Storage.HDD)
            {
                HddListUserControl list = new HddListUserControl();
                dbContext.Hdd.Load();
                list.dataGrid.ItemsSource = dbContext.Hdd.Local.ToList();
                ComponentChoiceWindow hddWindow = new ComponentChoiceWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Application.Current.MainWindow
                };
                hddWindow.ComponentsList.Content = list;
                if (hddWindow.ShowDialog() == true)
                {
                    if (list.dataGrid.SelectedItem is Hdd hdd && commandParameter is int index)
                        SetSata(index, hdd);
                }
            }
            else
            {
                SsdListUserControl list = new SsdListUserControl();
                dbContext.Ssd.Load();
                list.dataGrid.ItemsSource = dbContext.Ssd.Local.ToList();
                ComponentChoiceWindow ssdWindow = new ComponentChoiceWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Application.Current.MainWindow
                };
                ssdWindow.ComponentsList.Content = list;
                if (ssdWindow.ShowDialog() == true)
                {
                    if (list.dataGrid.SelectedItem is Ssd ssd && commandParameter is int index)
                        SetSata(index, ssd);
                }
            }
        }
    }

    private RelayCommand? checkCompatibility;
    public ICommand CheckCompatibility => checkCompatibility ??= new RelayCommand(PerformCheckCompatibility);
    private void PerformCheckCompatibility(object? commandParameter)
    {
        Configuration configuration = new Configuration
        {
            Motherboard = Motherboard,
            Cpu = Cpu,
            Cooler = Cooler,
            PowerSupply = PowerSupply
        };

        //RAM
        List<ConfigurationRam> rams = [];
        for (int i = 0; i < Rams?.Length; i++)
        {
            if (Rams[i] is Ram ram)
                rams.Add(new ConfigurationRam { Configuration = configuration, Ram = ram });
        }
        configuration.ConfigurationRams = rams;

        //GPU
        List<ConfigurationGpu> gpus = [];
        for (int i = 0; i < Gpus?.Length; i++)
        {
            if (Gpus[i] is Gpu gpu)
                gpus.Add(new ConfigurationGpu { Configuration = configuration, Gpu = gpu });
        }
        configuration.ConfigurationGpus = gpus;

        //M2SSD
        List<ConfigurationM2Ssd> m2ssds = [];
        for (int i = 0; i < M2Ssds?.Length; i++)
        {
            if (M2Ssds[i].M2Ssd is M2Ssd m2ssd)
                m2ssds.Add(new ConfigurationM2Ssd { Configuration = configuration, M2Slot = M2Ssds[i].M2Slot, M2Ssd = m2ssd });
        }
        configuration.ConfigurationM2Ssds = m2ssds;

        //Sata
        List<ConfigurationHdd> hdds = [];
        List<ConfigurationSsd> ssds = [];
        for (int i = 0; i < SsdAndHdds?.Length; i++)
        {
            if (SsdAndHdds[i] is Hdd hdd)
                hdds.Add(new ConfigurationHdd { Configuration = configuration, Hdd = hdd });
            else if (SsdAndHdds[i] is Ssd ssd)
                ssds.Add(new ConfigurationSsd { Configuration = configuration, Ssd = ssd });
        }
        configuration.ConfigurationHdds = hdds;
        configuration.ConfigurationSsds = ssds;

        Errors = configuration.CheckCompatibility();
    }
}

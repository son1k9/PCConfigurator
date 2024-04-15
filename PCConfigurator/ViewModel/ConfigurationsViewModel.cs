using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PCConfigurator.Commands;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.View;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json.Serialization;
using System.Windows.Data;
using System.Windows.Input;
using Newtonsoft.Json;

namespace PCConfigurator.ViewModel;

internal class ConfigurationsViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    private ConfigurationViewModel? _selectedConfiguration;
    public ConfigurationViewModel? SelectedConfiguration
    {
        get => _selectedConfiguration;
        set
        {
            if (_selectedConfiguration?.Configuration != value.Configuration)
            {
                if (_selectedConfiguration?.Changes == true)
                {
                    var result = MessageBox.Show("Сохранить изменения?", "Закрытие конфигурации", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        _selectedConfiguration.Save.Execute(null);
                        if (_selectedConfiguration.Changes)
                            return;
                    }
                    else if (result == MessageBoxResult.Cancel)
                        return;
                }

                _selectedConfiguration = value;
                _selectedConfiguration.ConfigurationSaved += (sender, e) => ViewSource.Refresh();
                OnPropertyChanged(nameof(SelectedConfiguration));
            }
        }
    }

    public ConfigurationsViewModel(int id = 0)
    {
        dbContext.Configuration.Load();
        _viewSource.Source = dbContext.Configuration.Local.ToObservableCollection();
        if (id != 0)
        {
            Configuration? configuration = dbContext.Configuration.Find(id);
            if (configuration != null)
                SelectedConfiguration = new ConfigurationViewModel(configuration, dbContext);
        }
    }

    private RelayCommand? _selectConfiguration;
    public ICommand SelectConfiguration => _selectConfiguration ??= new RelayCommand(PerformSelectConfiguration);

    private void PerformSelectConfiguration(object? commandParameter)
    {
        if (commandParameter is Configuration configuration)
            SelectedConfiguration = new ConfigurationViewModel(configuration, dbContext);
    }

    private RelayCommand? _addConfiguration;
    public ICommand AddConfiguration => _addConfiguration ??= new RelayCommand(PerformAddConfiguration);
    private void PerformAddConfiguration(object? commandParameter)
    {
        AddConfiguartionWindow windowChoice = new AddConfiguartionWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
        };

        if (windowChoice.ShowDialog() == true) 
        { 
            if (windowChoice.FromFile)
            {
                OpenFileDialog fileDialog = new OpenFileDialog()
                {
                    DefaultExt = ".json",
                    Filter = "Json files (*.json)|*.json"
                };

                if (fileDialog.ShowDialog() == true)
                {
                    string filename = fileDialog.FileName;
                    string jsonString = File.ReadAllText(filename);
                    Configuration? configuration = JsonConvert.DeserializeObject<Configuration>(jsonString);
                    if (configuration != null)
                    {
                        Configuration configuration1 = new Configuration() { Name = configuration.Name, };

                        Socket? socket = dbContext.Socket.FirstOrDefault(s => s.Name == configuration.Motherboard.Socket.Name);
                        if (socket == null)
                            socket = configuration.Motherboard.Socket;

                        Chipset? chipset = dbContext.Chipset.FirstOrDefault(c => c.Name == configuration.Motherboard.Chipset.Name);
                        if (chipset == null)
                        {
                            chipset = configuration.Motherboard.Chipset;
                            chipset.Socket = socket;
                        }

                        Cpu? cpu = dbContext.Cpu.FirstOrDefault(c => c.Model == configuration.Cpu.Model);
                        if (cpu == null)
                        {
                            cpu = configuration.Cpu;
                            cpu.Socket = socket;
                        }

                        PowerSupply? powerSupply = dbContext.PowerSupply.FirstOrDefault(p => p.Model == configuration.PowerSupply.Model);
                        if (powerSupply == null)
                            powerSupply = configuration.PowerSupply;

                        Cooler? cooler = dbContext.Cooler.FirstOrDefault(c => c.Model == configuration.Cooler.Model);
                        if (cooler == null)
                        {
                            cooler = configuration.Cooler;
                            cooler.Sockets = [socket];
                        }

                        List<ConfigurationRam> rams = [];
                        foreach (var ram in configuration.Rams)
                        {
                            Ram? ramFromDb = dbContext.Ram.FirstOrDefault(r => r.Model == ram.Model);
                            if (ramFromDb == null)
                            {
                                var ram1 = rams.FirstOrDefault(r=>r.Ram.Model == ram.Model);
                                if (ram1 == null)
                                    ramFromDb = ram;
                                else
                                    ramFromDb = ram1.Ram;
                            }
                            rams.Add(new ConfigurationRam() { Configuration = configuration1, Ram = ramFromDb});
                        }

                        List<ConfigurationGpu> gpus = [];
                        foreach (var gpu in configuration.Gpus)
                        {
                            Gpu? gpuFromDb = dbContext.Gpu.FirstOrDefault(g => g.Model == gpu.Model);
                            if (gpuFromDb == null)
                                gpuFromDb = gpu;
                            gpus.Add(new ConfigurationGpu() { Configuration = configuration1, Gpu = gpuFromDb });
                        }

                        List<ConfigurationHdd> hdds = [];
                        foreach (var hdd in configuration.Hdds)
                        {
                            Hdd? hddFromDb = dbContext.Hdd.FirstOrDefault(h => h.Model == hdd.Model);
                            if (hddFromDb == null)
                                hddFromDb = hdd;
                            hdds.Add(new ConfigurationHdd() { Configuration = configuration1, Hdd = hddFromDb });
                        }

                        List<ConfigurationSsd> ssds = [];
                        foreach (var ssd in configuration.Ssds)
                        {
                            Ssd? ssdFromDb = dbContext.Ssd.FirstOrDefault(s => s.Model == ssd.Model);
                            if (ssdFromDb == null)
                                ssdFromDb = ssd;
                            ssds.Add(new ConfigurationSsd() { Configuration = configuration1, Ssd = ssdFromDb });
                        }

                        List<ConfigurationM2Ssd> m2Ssds = [];
                        Motherboard? motherboard = dbContext.Motherboard.FirstOrDefault(m => m.Model == configuration.Motherboard.Model);
                        if (motherboard == null)
                        {
                            motherboard = configuration.Motherboard;
                            motherboard.Chipset = chipset;
                            motherboard.Socket = socket;

                            List<M2Slot> m2Slots = [];
                            foreach (var m2Ssd in configuration.ConfigurationM2Ssds)
                            {
                                m2Slots.Add(m2Ssd.M2Slot);
                                if (m2Ssd.M2Ssd is null)
                                    continue;

                                M2Ssd? m2SsdFromDb = dbContext.M2Ssd.FirstOrDefault(m => m.Model == m2Ssd.M2Ssd.Model);
                                if (m2SsdFromDb == null)
                                    m2SsdFromDb = m2Ssd.M2Ssd;

                                m2Ssds.Add(new ConfigurationM2Ssd() { Configuration = configuration1, M2Slot = m2Ssd.M2Slot, M2Ssd = m2SsdFromDb });
                            }

                            motherboard.M2Slots = m2Slots;
                        }
                        else
                        {
                            foreach (var m2Ssd in configuration.ConfigurationM2Ssds)
                            {
                                if (m2Ssd.M2Ssd is null)
                                    continue;

                                M2Ssd? m2SsdFromDb = dbContext.M2Ssd.FirstOrDefault(m => m.Model == m2Ssd.M2Ssd.Model);
                                if (m2SsdFromDb == null)
                                    m2SsdFromDb = m2Ssd.M2Ssd;
                                M2Slot m2Slot = motherboard.M2Slots.FirstOrDefault(m => m.M2Interface == m2Ssd.M2Slot.M2Interface && m.M2Size == m2Ssd.M2Slot.M2Size);
                                if (m2Slot == null || m2Ssds.Any(m => m.M2Slot.M2SlotId == m2Slot.M2SlotId))
                                {
                                    break;
                                }
                                m2Ssds.Add(new ConfigurationM2Ssd() { Configuration = configuration1, M2Slot = m2Slot, M2Ssd = m2SsdFromDb });
                            }
                        }

                        configuration1.Cpu = cpu;
                        configuration1.Cooler = cooler;
                        configuration1.Motherboard = motherboard;
                        configuration1.PowerSupply = powerSupply;
                        configuration1.ConfigurationRams = rams;
                        configuration1.ConfigurationGpus = gpus;
                        configuration1.ConfigurationSsds = ssds;
                        configuration1.ConfigurationHdds = hdds;
                        configuration1.ConfigurationM2Ssds = m2Ssds;


                        var errors = configuration1.CheckCompatibility();
                        if ( errors.Length > 0)
                        {
                            MessageBox.Show("Нельзя импортировать несовместимую сборку.", "Ошибка");
                            return;
                        }

                        SelectedConfiguration = new ConfigurationViewModel(configuration1, dbContext, true);

                        dbContext.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось импортировать сборку из файла", "Ошибка");
                    }
                }
            }
            else
            {
                Configuration configuration = new Configuration();
                ConfigurationNameInputWindow windowConfiguartion = new ConfigurationNameInputWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Application.Current.MainWindow,
                    DataContext = configuration
                };
                if (windowConfiguartion.ShowDialog() == true)
                {
                    SelectedConfiguration = new ConfigurationViewModel(configuration, dbContext, true);
                }
            }
        }
    }

    private RelayCommand? _removeConfiguration;
    public ICommand RemoveConfiguration => _removeConfiguration ??= new RelayCommand(PerformRemoveConfiguration);
    private void PerformRemoveConfiguration(object? commandParameter)
    {
        if (commandParameter is Configuration configuration)
        {
            var result = MessageBox.Show($"Удалить конфигурацию {configuration.Name}?", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (SelectedConfiguration?.Configuration == configuration)
                {
                    _selectedConfiguration = null;
                    OnPropertyChanged(nameof(SelectedConfiguration));
                }

                dbContext.Configuration.Remove(configuration);
                dbContext.SaveChanges();
            }
        }
    }

    private RelayCommand? _exportConfiguration;
    public ICommand ExportConfiguration => _exportConfiguration ??= new RelayCommand(PerformExportConfiguration);
    private void PerformExportConfiguration(object? commandParameter)
    {
        if (commandParameter is Configuration configuration)
        {
            if (configuration.CheckCompatibility().Length > 0)
            {
                MessageBox.Show("Нельзя экспортировать несовместимую сборку.", "Ошибка");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json"
            };

            if (saveDialog.ShowDialog() == true)
            {
                string path = saveDialog.FileName;

                Cooler cooler = new Cooler()
                {
                    Model = configuration.Cooler.Model,
                    Tdp = configuration.Cooler.Tdp,
                };

                Socket socket = new Socket()
                {
                    Name = configuration.Motherboard.Socket.Name,
                };

                Cpu cpu = new Cpu()
                {
                    Model = configuration.Cpu.Model,
                    Socket = socket,
                    Cores = configuration.Cpu.Cores,
                    ECores = configuration.Cpu.ECores,
                    Smt = configuration.Cpu.Smt,
                    CoreClock = configuration.Cpu.CoreClock,
                    BoostClock = configuration.Cpu.BoostClock,
                    L2Cache = configuration.Cpu.L2Cache,
                    L3Cache = configuration.Cpu.L3Cache,
                    Tdp = configuration.Cpu.Tdp,
                    RamTypes = configuration.Cpu.RamTypes,
                    MaxRamCapacity = configuration.Cpu.MaxRamCapacity,
                    HaveGraphics = configuration.Cpu.HaveGraphics
                };

                Chipset chipset = new Chipset()
                {
                    Name = configuration.Motherboard.Chipset.Name
                };

                Motherboard motherboard = new Motherboard()
                {
                    Model = configuration.Motherboard.Model,
                    Socket = socket,
                    Chipset = chipset,
                    RamType = configuration.Motherboard.RamType,
                    RamSlots = configuration.Motherboard.RamSlots,
                    MaxRamCapacity = configuration.Motherboard.MaxRamCapacity,
                    Sata3Ports = configuration.Motherboard.Sata3Ports,
                    PCIex16Slots = configuration.Motherboard.PCIex16Slots
                };

                List<Gpu> gpus = [];
                foreach (var gpu in configuration.ConfigurationGpus)
                {
                    gpus.Add(new Gpu()
                    {
                        Model = gpu.Gpu.Model,
                        CoreClock = gpu.Gpu.CoreClock,
                        BoostClock = gpu.Gpu.BoostClock,
                        VramCapacity = gpu.Gpu.VramCapacity,
                        PowerConsumption = gpu.Gpu.PowerConsumption
                    });
                }

                List<Hdd> hdds = [];
                foreach (var hdd in configuration.ConfigurationHdds)
                {
                    hdds.Add(new Hdd()
                    {
                        Model = hdd.Hdd.Model,
                        Capacity = hdd.Hdd.Capacity,
                        SpindelSpeed = hdd.Hdd.SpindelSpeed
                    });
                }

                List<ConfigurationM2Ssd> m2ssds = [];
                foreach (var slot in configuration.Motherboard.M2Slots)
                {
                    ConfigurationM2Ssd? m2Ssd = configuration.ConfigurationM2Ssds.FirstOrDefault(c => c.M2SlotId == slot.M2SlotId);
                    M2Ssd? m2Ssd1 = null;
                    if (m2Ssd != null)
                    {
                        m2Ssd1 = new M2Ssd()
                        {
                            Model = m2Ssd.M2Ssd.Model,
                            Capacity = m2Ssd.M2Ssd.Capacity,
                            ReadSpeed = m2Ssd.M2Ssd.ReadSpeed,
                            WriteSpeed = m2Ssd.M2Ssd.WriteSpeed,
                            Tbw = m2Ssd.M2Ssd.Tbw,
                            NandType = m2Ssd.M2Ssd.NandType,
                            M2Interface = m2Ssd.M2Ssd.M2Interface,
                            M2Size = m2Ssd.M2Ssd.M2Size
                        };
                    }
                    m2ssds.Add(new ConfigurationM2Ssd()
                    {
                        M2Slot = new M2Slot()
                        {
                            M2Interface = slot.M2Interface,
                            M2Size = slot.M2Size
                        },
                        M2Ssd = m2Ssd1
                    });
                }

                PowerSupply powerSupply = new PowerSupply()
                {
                    Model = configuration.PowerSupply.Model,
                    Wattage = configuration.PowerSupply.Wattage
                };

                List<Ram> rams = [];
                foreach (var ram in configuration.ConfigurationRams)
                {
                    rams.Add(new Ram()
                    {
                        Model = ram.Ram.Model,
                        RamType = ram.Ram.RamType,
                        Capacity = ram.Ram.Capacity,
                        Clock = ram.Ram.Clock,
                        Cl = ram.Ram.Cl
                    });
                }

                List<Ssd> ssds = [];
                foreach (var ssd in configuration.ConfigurationSsds)
                {
                    ssds.Add(new Ssd()
                    {
                        Model = ssd.Ssd.Model,
                        Capacity = ssd.Ssd.Capacity,
                        ReadSpeed = ssd.Ssd.ReadSpeed,
                        WriteSpeed = ssd.Ssd.WriteSpeed,
                        Tbw = ssd.Ssd.Tbw,
                        NandType = ssd.Ssd.NandType
                    });
                }

                Configuration configuration1 = new Configuration()
                {
                    Name = configuration.Name,
                    Motherboard = motherboard,
                    Cpu = cpu,
                    Cooler = cooler,
                    PowerSupply = powerSupply,
                    Rams = rams,
                    Gpus = gpus,
                    Ssds = ssds,
                    Hdds = hdds,
                    ConfigurationM2Ssds = m2ssds
                };

                string jsonString = JsonConvert.SerializeObject(configuration1, Formatting.Indented);

                using StreamWriter file = File.CreateText(path);
                file.Write(jsonString);
            }
        }
    }
}


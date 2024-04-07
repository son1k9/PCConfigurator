using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using System.Windows;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

internal class ConfigurationViewModel : BaseViewModel
{
    Configuration _configuration;

    public Configuration Configuration { get => _configuration; }

    Action _saveChanges;

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
            _rams[i] = new Ram() { Model = "None"};



    }

    public string Name { get; set; }

    public Motherboard Motherboard { get; set; }

    public Cpu Cpu { get; set; }

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


    private RelayCommand _save;
    public ICommand Save => _save ??= new RelayCommand(PerformSave);

    private void PerformSave(object? commandParameter)
    {
        _configuration.Name = Name;
        _saveChanges.Invoke();
    }

    private RelayCommand _test;
    public ICommand Test => _test ??= new RelayCommand(PerformTest);

    private void PerformTest(object? commandParameter)
    {
        MessageBox.Show(commandParameter?.ToString());
    }
}

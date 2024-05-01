using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PCConfigurator.Commands;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.View;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Newtonsoft.Json;
using PCConfigurator.Helper;

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
                ImportConfiguration();
            else
                PerfromCreateConfiguration();
        }
    }

    private void ImportConfiguration()
    {
        OpenFileDialog fileDialog = new OpenFileDialog()
        {
            DefaultExt = ".json",
            Filter = "Json files (*.json)|*.json"
        };

        if (fileDialog.ShowDialog() == true)
        {
            string filename = fileDialog.FileName;
            try
            {
                Configuration? configuration = ConfigurationIE.Import(filename, dbContext);
                if (configuration != null)
                {
                    SelectedConfiguration = new ConfigurationViewModel(configuration, dbContext, true);
                    dbContext.SaveChanges();
                }
                else
                    MessageBox.Show("Не удалось импортировать сборку из файла.", "Ошибка");
            }
            catch
            {
                MessageBox.Show("Не удалось открыть файл.", "Ошибка");
            }
        }
    }

    private void PerfromCreateConfiguration()
    {
        Configuration configuration = new Configuration();
        ConfigurationNameInputWindow windowConfiguartion = new ConfigurationNameInputWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = configuration
        };
        if (windowConfiguartion.ShowDialog() == true)
            SelectedConfiguration = new ConfigurationViewModel(configuration, dbContext, true);
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
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json"
            };

            if (saveDialog.ShowDialog() == true)
            {
                string path = saveDialog.FileName;
                if (!ConfigurationIE.Export(path, configuration))
                    MessageBox.Show("Нельзя экспортировать несовместимую сборку.", "Ошибка");
            }
        }
    }
}
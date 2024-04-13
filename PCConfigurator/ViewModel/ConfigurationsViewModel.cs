using Microsoft.EntityFrameworkCore;
using PCConfigurator.Commands;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PCConfigurator.ViewModel;

internal class ConfigurationsViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }



    private ConfigurationViewModel _selectedConfiguration;
    public ConfigurationViewModel SelectedConfiguration
    {
        get => _selectedConfiguration;
        set
        {
            if (_selectedConfiguration?.Configuration != value.Configuration)
            {
                if (_selectedConfiguration?.Changes == true)
                {
                    var result = MessageBox.Show("Сохранить изменения?", "Смена конфигурации", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                        _selectedConfiguration.Save.Execute(null);
                    else if (result == MessageBoxResult.Cancel)
                        return;
                }

                _selectedConfiguration = value;
                _selectedConfiguration.ConfigurationSaved += (sender, e) => ViewSource.Refresh();
                OnPropertyChanged(nameof(SelectedConfiguration));
            }
        }
    }

    public ConfigurationsViewModel()
    {
        dbContext.Configuration.Load();
        _viewSource.Source = dbContext.Configuration.Local.ToObservableCollection();
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

    }
}

using Microsoft.EntityFrameworkCore;
using PCConfigurator.Commands;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.View.AddComponents;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.ComponentsViewModels;

internal class PowerSupplyViewModel : BaseViewModel
{
    private ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public PowerSupplyViewModel()
    {
        dbContext.PowerSupply.Load();
        _viewSource.Source = dbContext.PowerSupply.Local.ToObservableCollection();
    }

    private void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.PowerSupply.Load();
        _viewSource.Source = dbContext.PowerSupply.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private void PerformAdd(object? commandParameter)
    {
        PowerSupply powerSupply = new PowerSupply();

        NewPowerSupplyWindow window = new NewPowerSupplyWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = powerSupply
        };

        if (window.ShowDialog() == true)
        {
            dbContext.PowerSupply.Add(powerSupply);
            dbContext.SaveChanges();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private void PerformRemove(object? commandParameter)
    {
        if (commandParameter is PowerSupply powerSupply)
        {
            var result = MessageBox.Show($"Удалить данные по {powerSupply.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (powerSupply.Configurations.Count > 0)
                    MessageBox.Show("Нельзя удалить блок питания, так как он используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.PowerSupply.Remove(powerSupply);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private void PerformEdit(object? commandParameter)
    {
        if (commandParameter is PowerSupply powerSupply)
        {
            NewPowerSupplyWindow window = new NewPowerSupplyWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = powerSupply
            };

            if (window.ShowDialog() == true)
                dbContext.SaveChanges();

            ResetContext();
        }
    }
}

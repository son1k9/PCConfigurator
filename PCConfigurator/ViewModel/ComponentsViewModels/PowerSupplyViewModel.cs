using Microsoft.EntityFrameworkCore;
using PCConfigurator.Commands;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.View.AddComponents;
using PCConfigurator.ViewModel.NewComponentsViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.ComponentsViewModels;

internal class PowerSupplyViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public PowerSupplyViewModel()
    {
        dbContext.PowerSupply.Load();
        _viewSource.Source = dbContext.PowerSupply.Local.ToObservableCollection();
    }

    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private async void PerformAdd(object? commandParameter)
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
            await dbContext.PowerSupply.AddAsync(powerSupply);
            await dbContext.SaveChangesAsync();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private async void PerformRemove(object? commandParameter)
    {
        if (commandParameter is PowerSupply powerSupply)
        {
            var result = MessageBox.Show($"Удалить данные по {powerSupply.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbContext.PowerSupply.Remove(powerSupply);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private async void PerformEdit(object? commandParameter)
    {
        if (commandParameter is PowerSupply powerSupply)
        {
            PowerSupply powerSupplyCopy = powerSupply.Clone();

            NewPowerSupplyWindow window = new NewPowerSupplyWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = powerSupplyCopy
            };

            if (window.ShowDialog() == true)
            {
                dbContext.PowerSupply.Entry(powerSupply).CurrentValues.SetValues(powerSupplyCopy);
                await dbContext.SaveChangesAsync();
                ViewSource.Refresh();
            }
        }
    }
}

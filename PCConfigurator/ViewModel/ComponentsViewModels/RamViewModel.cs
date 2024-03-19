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

internal class RamViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public RamViewModel()
    {
        dbContext.Ram.Load();
        _viewSource.Source = dbContext.Ram.Local.ToObservableCollection();
    }

    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private async void PerformAdd(object? commandParameter)
    {
        Ram ram = new Ram() { RamType = RamType.DDR4 };

        NewRamWindow window = new NewRamWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = ram
        };

        if (window.ShowDialog() == true)
        {
            await dbContext.Ram.AddAsync(ram);
            await dbContext.SaveChangesAsync();
        }
    }
}

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

internal class CoolerViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public CoolerViewModel()
    {
        dbContext.Cooler.Load();
        _viewSource.Source = dbContext.Cooler.Local.ToObservableCollection();
    }

    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private async void PerformAdd(object? commandParameter)
    {
        Cooler cooler = new Cooler();
        NewCoolerViewmodel coolerViewmodel = new NewCoolerViewmodel(cooler);

        NewCoolerWindow window = new NewCoolerWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = coolerViewmodel
        };

        dbContext.Socket.Load();
        window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

        if (window.ShowDialog() == true)
        {
            await dbContext.Cooler.AddAsync(cooler);
            await dbContext.SaveChangesAsync();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private async void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Cooler cooler)
        {
            var result = MessageBox.Show($"Удалить данные по {cooler.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbContext.Cooler.Remove(cooler);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private async void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Cooler cooler)
        {
            Cooler coolerCopy = cooler.Clone();
            NewCoolerViewmodel coolerViewmodel = new(coolerCopy);
            NewCoolerWindow window = new NewCoolerWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = coolerViewmodel
            };

            await dbContext.Socket.LoadAsync();

            window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

            if (window.ShowDialog() == true)
            {
                dbContext.Cooler.Entry(cooler).CurrentValues.SetValues(coolerCopy);
                cooler.Sockets = coolerCopy.Sockets;
                await dbContext.SaveChangesAsync();
                ViewSource.Refresh();
            }
        }
    }
}

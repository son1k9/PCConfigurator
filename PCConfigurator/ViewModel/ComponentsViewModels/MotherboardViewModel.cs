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

internal class MotherboardViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public MotherboardViewModel()
    {
        dbContext.Motherboard.Load();
        _viewSource.Source = dbContext.Motherboard.Local.ToObservableCollection();
    }


    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private async void PerformAdd(object? commandParameter)
    {
        Motherboard motherboard = new Motherboard();
        NewMotherboardViewmodel motherboardViewmodel = new(motherboard);

        NewMotherboardWindow window = new NewMotherboardWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = motherboardViewmodel
        };

        await dbContext.Socket.LoadAsync();
        await dbContext.Chipset.LoadAsync();

        window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

        if (window.ShowDialog() == true)
        {
            await dbContext.Motherboard.AddAsync(motherboard);
            await dbContext.SaveChangesAsync();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private async void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Motherboard motherboard)
        {
            var result = MessageBox.Show($"Удалить данные по {motherboard.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbContext.Motherboard.Remove(motherboard);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private async void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Motherboard motherboard)
        {
            Motherboard motherboardCopy = motherboard.Clone();
            NewMotherboardViewmodel motherboardViewmodel = new(motherboardCopy);
            NewMotherboardWindow window = new NewMotherboardWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = motherboardViewmodel
            };

            await dbContext.Socket.LoadAsync();
            await dbContext.Chipset.LoadAsync();

            window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

            if (window.ShowDialog() == true)
            {
                dbContext.Motherboard.Entry(motherboard).CurrentValues.SetValues(motherboardCopy);
                motherboard.Chipset = motherboardCopy.Chipset;
                motherboard.Socket = motherboardCopy.Socket;
                motherboard.M2Slots = motherboardCopy.M2Slots;
                await dbContext.SaveChangesAsync();
                ViewSource.Refresh();
            }
        }
    }
}
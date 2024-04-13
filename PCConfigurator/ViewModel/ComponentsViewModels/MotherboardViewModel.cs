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
    private ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public MotherboardViewModel()
    {
        dbContext.Motherboard.Load();
        _viewSource.Source = dbContext.Motherboard.Local.ToObservableCollection();
    }

    private void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.Motherboard.Load();
        _viewSource.Source = dbContext.Motherboard.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private void PerformAdd(object? commandParameter)
    {
        Motherboard motherboard = new Motherboard();
        NewMotherboardViewmodel motherboardViewmodel = new(motherboard);

        NewMotherboardWindow window = new NewMotherboardWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = motherboardViewmodel
        };

        dbContext.Socket.Load();
        dbContext.Chipset.Load();

        window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

        if (window.ShowDialog() == true)
        {
            dbContext.Motherboard.Add(motherboard);
            dbContext.SaveChanges();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Motherboard motherboard)
        {
            var result = MessageBox.Show($"Удалить данные по {motherboard.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (motherboard.Configurations.Count > 0)
                    MessageBox.Show("Нельзя удалить материнскую плату, так как она используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.Motherboard.Remove(motherboard);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Motherboard motherboard)
        {
            NewMotherboardViewmodel motherboardViewmodel = new(motherboard);
            NewMotherboardWindow window = new NewMotherboardWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = motherboardViewmodel
            };

            dbContext.Socket.Load();
            dbContext.Chipset.Load();

            window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

            if (window.ShowDialog() == true)
            {
                if (motherboardViewmodel.RemovedM2Slots.Any(slot => slot.ConfigurationM2Ssds.Count > 0))
                    MessageBox.Show(Application.Current.MainWindow, "Неудалось обновить информацию о материнской плате, " +
                        "так как при этом нарушалась бы целостность конфигураций.", "Ошибка");
                else
                    dbContext.SaveChanges();
            }
            ResetContext();
        }
    }
}
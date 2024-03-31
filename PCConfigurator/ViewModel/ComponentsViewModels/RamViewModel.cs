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
    private void PerformAdd(object? commandParameter)
    {
        Ram ram = new Ram();

        NewRamWindow window = new NewRamWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = ram
        };

        if (window.ShowDialog() == true)
        {
            dbContext.Ram.Add(ram);
            dbContext.SaveChanges();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Ram ram)
        {
            var result = MessageBox.Show($"Удалить данные по {ram.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbContext.Ram.Remove(ram);
                dbContext.SaveChanges();
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Ram ram)
        {
            Ram ramCopy = ram.Clone();
            NewRamWindow window = new NewRamWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = ramCopy
            };

            if (window.ShowDialog() == true)
            {
                dbContext.Ram.Entry(ram).CurrentValues.SetValues(ramCopy);
                dbContext.SaveChanges();
                ViewSource.Refresh();
            }
        }
    }
}

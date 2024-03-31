using PCConfigurator.Commands;
using PCConfigurator.Model.Components;
using PCConfigurator.Model;
using PCConfigurator.View.AddComponents;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace PCConfigurator.ViewModel.ComponentsViewModels;

internal class HddViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public HddViewModel()
    {
        dbContext.Hdd.Load();
        _viewSource.Source = dbContext.Hdd.Local.ToObservableCollection();
    }

    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private void PerformAdd(object? commandParameter)
    {
        Hdd hdd = new Hdd();

        NewHddWindow window = new NewHddWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = hdd
        };

        if (window.ShowDialog() == true)
        {
            dbContext.Hdd.Add(hdd);
            dbContext.SaveChanges();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Hdd hdd)
        {
            var result = MessageBox.Show($"Удалить данные по {hdd.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbContext.Hdd.Remove(hdd);
                dbContext.SaveChanges();
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Hdd hdd)
        {
            Hdd hddCopy = hdd.Clone();
            NewHddWindow window = new NewHddWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = hddCopy
            };

            if (window.ShowDialog() == true)
            {
                dbContext.Hdd.Entry(hdd).CurrentValues.SetValues(hddCopy);
                dbContext.SaveChanges();
                ViewSource.Refresh();
            }
        }
    }
}

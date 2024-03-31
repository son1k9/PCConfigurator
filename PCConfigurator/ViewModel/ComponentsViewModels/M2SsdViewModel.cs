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

internal class M2SsdViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public M2SsdViewModel()
    {
        dbContext.M2Ssd.Load();
        _viewSource.Source = dbContext.M2Ssd.Local.ToObservableCollection();
    }


    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private void PerformAdd(object? commandParameter)
    {
        M2Ssd m2ssd = new M2Ssd();

        NewM2SsdWindow window = new NewM2SsdWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = m2ssd
        };

        if (window.ShowDialog() == true)
        {
            dbContext.M2Ssd.Add(m2ssd);
            dbContext.SaveChanges();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private void PerformRemove(object? commandParameter)
    {
        if (commandParameter is M2Ssd m2ssd)
        {
            var result = MessageBox.Show($"Удалить данные по {m2ssd.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbContext.M2Ssd.Remove(m2ssd);
                dbContext.SaveChanges();
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private void PerformEdit(object? commandParameter)
    {
        if (commandParameter is M2Ssd m2ssd)
        {
            M2Ssd m2ssdCopy = m2ssd.Clone();
            NewM2SsdWindow window = new NewM2SsdWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = m2ssdCopy
            };

            if (window.ShowDialog() == true)
            {
                dbContext.M2Ssd.Entry(m2ssd).CurrentValues.SetValues(m2ssdCopy);
                dbContext.SaveChanges();
                ViewSource.Refresh();
            }
        }
    }
}

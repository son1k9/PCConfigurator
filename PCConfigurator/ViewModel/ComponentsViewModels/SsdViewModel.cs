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

internal class SsdViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public SsdViewModel()
    {
        dbContext.Ssd.Load();
        _viewSource.Source = dbContext.Ssd.Local.ToObservableCollection();
    }


    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private void PerformAdd(object? commandParameter)
    {
        Ssd ssd = new Ssd();
        NewSsdViewModel viewModel = new NewSsdViewModel(ssd);

        NewSsdWindow window = new NewSsdWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = viewModel
        };

        if (window.ShowDialog() == true)
        {
            dbContext.Ssd.Add(ssd);
            dbContext.SaveChanges();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Ssd ssd)
        {
            var result = MessageBox.Show($"Удалить данные по {ssd.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbContext.Ssd.Remove(ssd);
                dbContext.SaveChanges();
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Ssd ssd)
        {
            Ssd ssdCopy = ssd.Clone();
            NewSsdViewModel viewModel = new NewSsdViewModel(ssdCopy);
            NewSsdWindow window = new NewSsdWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = viewModel
            };

            if (window.ShowDialog() == true)
            {
                dbContext.Ssd.Entry(ssd).CurrentValues.SetValues(ssdCopy);
                dbContext.SaveChanges();
                ViewSource.Refresh();
            }
        }
    }
}

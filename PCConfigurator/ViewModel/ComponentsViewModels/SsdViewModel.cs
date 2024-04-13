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
    private ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public SsdViewModel()
    {
        dbContext.Ssd.Load();
        _viewSource.Source = dbContext.Ssd.Local.ToObservableCollection();
    }

    private void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.Ssd.Load();
        _viewSource.Source = dbContext.Ssd.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
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
                if (ssd.Configurations.Count > 0)
                    MessageBox.Show("Нельзя удалить комплектующее, так как оно используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.Ssd.Remove(ssd);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Ssd ssd)
        {
            NewSsdViewModel viewModel = new NewSsdViewModel(ssd);
            NewSsdWindow window = new NewSsdWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = viewModel
            };

            if (window.ShowDialog() == true)
                dbContext.SaveChanges();

            ResetContext();
        }
    }
}

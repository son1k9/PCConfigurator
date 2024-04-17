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

internal class SsdViewModel : ComponentViewModel
{
    public SsdViewModel()
    {
        dbContext.Ssd.Load();
        _viewSource.Source = dbContext.Ssd.Local.ToObservableCollection();
    }

    protected override void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.Ssd.Load();
        _viewSource.Source = dbContext.Ssd.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    protected override void PerformAdd(object? commandParameter)
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

    protected override void PerformRemove(object? commandParameter)
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

    protected override void PerformEdit(object? commandParameter)
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

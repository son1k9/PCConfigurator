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

internal class HddViewModel : ComponentViewModel
{
    public HddViewModel()
    {
        dbContext.Hdd.Load();
        _viewSource.Source = dbContext.Hdd.Local.ToObservableCollection();
    }

    protected override void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.Hdd.Load();
        _viewSource.Source = dbContext.Hdd.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    protected override void PerformAdd(object? commandParameter)
    {
        Hdd hdd = new Hdd();
        NewHddViewModel viewModel = new NewHddViewModel(hdd);

        NewHddWindow window = new NewHddWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = viewModel
        };

        if (window.ShowDialog() == true)
        {
            dbContext.Hdd.Add(hdd);
            dbContext.SaveChanges();
        }
    }

    protected override void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Hdd hdd)
        {
            var result = MessageBox.Show($"Удалить данные по {hdd.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (hdd.Configurations.Count > 0)
                    MessageBox.Show("Нельзя удалить комплектующее, так как оно используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.Hdd.Remove(hdd);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    protected override void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Hdd hdd)
        {
            NewHddViewModel viewModel = new NewHddViewModel(hdd);
            NewHddWindow window = new NewHddWindow
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

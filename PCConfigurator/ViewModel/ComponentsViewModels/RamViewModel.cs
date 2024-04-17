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

internal class RamViewModel : ComponentViewModel
{
    public RamViewModel()
    {
        dbContext.Ram.Load();
        _viewSource.Source = dbContext.Ram.Local.ToObservableCollection();
    }

    protected override void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.Ram.Load();
        _viewSource.Source = dbContext.Ram.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    protected override void PerformAdd(object? commandParameter)
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

    protected override void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Ram ram)
        {
            var result = MessageBox.Show($"Удалить данные по {ram.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (ram.Configurations.Count > 0)
                    MessageBox.Show("Нельзя удалить комплектующее, так как оно используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.Ram.Remove(ram);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    protected override void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Ram ram)
        {
            NewRamWindow window = new NewRamWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = ram
            };

            int i = 0;
            bool[] beforeChanges = new bool[ram.Configurations.Count];
            foreach (var configuration in ram.Configurations)
            {
                if (configuration.CheckCompatibility().Length > 0)
                    beforeChanges[i++] = true;
                else
                    beforeChanges[i++] = false;
            }

            if (window.ShowDialog() == true)
            {
                bool[] afterChanges = new bool[ram.Configurations.Count];
                i = 0;
                foreach (var configuration in ram.Configurations)
                {
                    if (configuration.CheckCompatibility().Length > 0)
                        afterChanges[i++] = true;
                    else
                        afterChanges[i++] = false;
                }

                bool error = false;
                for (int j = 0; j < beforeChanges.Length; j++)
                {
                    if (beforeChanges[j] == false && afterChanges[j] == true)
                    {
                        error = true;
                        break;
                    }
                }

                if (error)
                    MessageBox.Show("Неудалось редактировать данные о комплектующем, так как некоторые конфигурации стали бы не совместимыми.", "Ошибка");
                else
                    dbContext.SaveChanges();
            }

            ResetContext();
        }
    }
}

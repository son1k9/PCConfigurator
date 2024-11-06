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

public class M2SsdViewModel : ComponentViewModel
{
    public M2SsdViewModel()
    {
        dbContext.M2Ssd.Load();
        _viewSource.Source = dbContext.M2Ssd.Local.ToObservableCollection();
    }

    protected override void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.M2Ssd.Load();
        _viewSource.Source = dbContext.M2Ssd.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    protected override void PerformAdd(object? commandParameter)
    {
        M2Ssd m2ssd = new M2Ssd();
        NewM2SsdViewModel viewModel = new NewM2SsdViewModel(m2ssd);

        NewM2SsdWindow window = new NewM2SsdWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = viewModel
        };

        if (window.ShowDialog() == true)
        {
            dbContext.M2Ssd.Add(m2ssd);
            dbContext.SaveChanges();
        }
    }

    protected override void PerformRemove(object? commandParameter)
    {
        if (commandParameter is M2Ssd m2ssd)
        {
            var result = MessageBox.Show($"Удалить данные по {m2ssd.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (m2ssd.ConfigurationM2Ssds.Count > 0)
                    MessageBox.Show("Нельзя удалить комплектующее, так как оно используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.M2Ssd.Remove(m2ssd);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    protected override void PerformEdit(object? commandParameter)
    {
        if (commandParameter is M2Ssd m2ssd)
        {
            NewM2SsdViewModel viewModel = new NewM2SsdViewModel(m2ssd);
            NewM2SsdWindow window = new NewM2SsdWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = viewModel
            };

            int i = 0;
            bool[] beforeChanges = new bool[m2ssd.ConfigurationM2Ssds.Count];
            foreach (var configuration in m2ssd.ConfigurationM2Ssds)
            {
                if (configuration.Configuration.CheckCompatibility().Length > 0)
                    beforeChanges[i++] = true;
                else
                    beforeChanges[i++] = false;
            }

            if (window.ShowDialog() == true)
            {
                bool[] afterChanges = new bool[m2ssd.ConfigurationM2Ssds.Count];
                i = 0;
                foreach (var configuration in m2ssd.ConfigurationM2Ssds)
                {
                    if (configuration.Configuration.CheckCompatibility().Length > 0)
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

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

internal class CpuViewModel : ComponentViewModel
{
    public CpuViewModel()
    {
        dbContext.Cpu.Load();
        _viewSource.Source = dbContext.Cpu.Local.ToObservableCollection();
    }

    protected override void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.Cpu.Load();
        _viewSource.Source = dbContext.Cpu.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    protected override void PerformAdd(object? commandParameter)
    {
        Cpu cpu = new Cpu();

        NewCpuWindow window = new NewCpuWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = cpu
        };

        dbContext.Socket.Load();

        window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

        if (window.ShowDialog() == true)
        {
            dbContext.Cpu.Add(cpu);
            dbContext.SaveChanges();
        }
    }

    protected override void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Cpu cpu)
        {
            var result = MessageBox.Show($"Удалить данные по {cpu.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (cpu.Configurations.Count > 0)
                    MessageBox.Show("Нельзя удалить комплектующее, так как оно используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.Cpu.Remove(cpu);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    protected override void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Cpu cpu)
        {
            NewCpuWindow window = new NewCpuWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = cpu
            };

            dbContext.Socket.Load();

            window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

            int i = 0;
            bool[] beforeChanges = new bool[cpu.Configurations.Count];
            foreach (var configuration in cpu.Configurations)
            {
                if (configuration.CheckCompatibility().Length > 0)
                    beforeChanges[i++] = true;
                else
                    beforeChanges[i++] = false;
            }

            if (window.ShowDialog() == true)
            {
                bool[] afterChanges = new bool[cpu.Configurations.Count];
                i = 0;
                foreach (var configuration in cpu.Configurations)
                {
                    if (configuration.CheckCompatibility().Length > 0)
                        afterChanges[i++] = true;
                    else
                        afterChanges[i++] = false;
                }

                bool error = false;
                for(int j = 0; j < beforeChanges.Length; j++) 
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

﻿using Microsoft.EntityFrameworkCore;
using PCConfigurator.Commands;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.View.AddComponents;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.ComponentsViewModels;

public class PowerSupplyViewModel : ComponentViewModel
{
    public PowerSupplyViewModel()
    {
        dbContext.PowerSupply.Load();
        _viewSource.Source = dbContext.PowerSupply.Local.ToObservableCollection();
    }

    protected override void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.PowerSupply.Load();
        _viewSource.Source = dbContext.PowerSupply.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    protected override void PerformAdd(object? commandParameter)
    {
        PowerSupply powerSupply = new PowerSupply();

        NewPowerSupplyWindow window = new NewPowerSupplyWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = powerSupply
        };

        if (window.ShowDialog() == true)
        {
            dbContext.PowerSupply.Add(powerSupply);
            dbContext.SaveChanges();
        }
    }

    protected override void PerformRemove(object? commandParameter)
    {
        if (commandParameter is PowerSupply powerSupply)
        {
            var result = MessageBox.Show($"Удалить данные по {powerSupply.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (powerSupply.Configurations.Count > 0)
                    MessageBox.Show("Нельзя удалить комплектующее, так как оно используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.PowerSupply.Remove(powerSupply);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    protected override void PerformEdit(object? commandParameter)
    {
        if (commandParameter is PowerSupply powerSupply)
        {
            NewPowerSupplyWindow window = new NewPowerSupplyWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = powerSupply
            };

            int i = 0;
            bool[] beforeChanges = new bool[powerSupply.Configurations.Count];
            foreach (var configuration in powerSupply.Configurations)
            {
                if (configuration.CheckCompatibility().Length > 0)
                    beforeChanges[i++] = true;
                else
                    beforeChanges[i++] = false;
            }

            if (window.ShowDialog() == true)
            {
                bool[] afterChanges = new bool[powerSupply.Configurations.Count];
                i = 0;
                foreach (var configuration in powerSupply.Configurations)
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

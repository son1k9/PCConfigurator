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

internal class GpuViewModel : BaseViewModel
{
    private ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public GpuViewModel()
    {
        dbContext.Gpu.Load();
        _viewSource.Source = dbContext.Gpu.Local.ToObservableCollection();
    }

    private void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.Gpu.Load();
        _viewSource.Source = dbContext.Gpu.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private void PerformAdd(object? commandParameter)
    {
        Gpu gpu = new Gpu();

        NewGpuWindow window = new NewGpuWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = gpu
        };

        if (window.ShowDialog() == true)
        {
            dbContext.Gpu.Add(gpu);
            dbContext.SaveChanges();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Gpu gpu)
        {
            var result = MessageBox.Show($"Удалить данные по {gpu.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (gpu.Configurations.Count > 0)
                    MessageBox.Show("Нельзя удалить комплектующее, так как оно используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.Gpu.Remove(gpu);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Gpu gpu)
        {
            NewGpuWindow window = new NewGpuWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = gpu
            };

            int i = 0;
            bool[] beforeChanges = new bool[gpu.Configurations.Count];
            foreach (var configuration in gpu.Configurations)
            {
                if (configuration.CheckCompatibility().Length > 0)
                    beforeChanges[i++] = true;
                else
                    beforeChanges[i++] = false;
            }

            if (window.ShowDialog() == true)
            {
                bool[] afterChanges = new bool[gpu.Configurations.Count];
                i = 0;
                foreach (var configuration in gpu.Configurations)
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

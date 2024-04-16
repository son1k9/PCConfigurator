﻿using Microsoft.EntityFrameworkCore;
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

internal class CoolerViewModel : BaseViewModel
{
    private ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public CoolerViewModel()
    {
        dbContext.Cooler.Load();
        _viewSource.Source = dbContext.Cooler.Local.ToObservableCollection();
    }

    private void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.Cooler.Load();
        _viewSource.Source = dbContext.Cooler.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private void PerformAdd(object? commandParameter)
    {
        Cooler cooler = new Cooler();
        NewCoolerViewmodel coolerViewmodel = new NewCoolerViewmodel(cooler);

        NewCoolerWindow window = new NewCoolerWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = coolerViewmodel
        };

        dbContext.Socket.Load();
        window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

        if (window.ShowDialog() == true)
        {
            dbContext.Cooler.Add(cooler);
            dbContext.SaveChanges();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Cooler cooler)
        {
            var result = MessageBox.Show($"Удалить данные по {cooler.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (cooler.Configurations.Count > 0)
                    MessageBox.Show("Нельзя удалить комплектующее, так как оно используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.Cooler.Remove(cooler);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Cooler cooler)
        {
            NewCoolerViewmodel coolerViewmodel = new(cooler);
            NewCoolerWindow window = new NewCoolerWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = coolerViewmodel
            };

            dbContext.Socket.Load();

            window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

            int i = 0;
            bool[] beforeChanges = new bool[cooler.Configurations.Count];
            foreach(var configuration in cooler.Configurations)
            {
                if (configuration.CheckCompatibility().Length > 0)
                    beforeChanges[i++] = true;
                else
                    beforeChanges[i++] = false;
            }

            if (window.ShowDialog() == true)
            {
                bool[] afterChanges = new bool[cooler.Configurations.Count];
                i = 0;
                foreach (var configuration in cooler.Configurations)
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

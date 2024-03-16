﻿using Microsoft.EntityFrameworkCore;
using PCConfigurator.Commands;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.View.AddComponents;
using PCConfigurator.ViewModel.NewComponentsViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.ComponentsViewModels
{
    internal class MotherboardViewModel : BaseViewModel
    {
        private readonly ApplicationContext dbContext = new ApplicationContext();

        private readonly CollectionViewSource _viewSource = new CollectionViewSource();

        public ICollectionView ViewSource { get => _viewSource.View; }

        public MotherboardViewModel()
        {
            dbContext.Motherboard.Load();
            _viewSource.Source = dbContext.Motherboard.Local.ToObservableCollection();
        }


        private RelayCommand _add;
        public ICommand Add => _add ??= new RelayCommand(PerformAdd);

        private void PerformAdd(object? commandParameter)
        {
            Motherboard motherboard = new Motherboard();
            NewMotherboardViewmodel motherboardViewmodel = new(motherboard);

           NewMotherboardWindow window = new NewMotherboardWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = motherboardViewmodel
           };

            dbContext.Socket.Load();
            dbContext.Chipset.Load();

            window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();
            window.comboBoxChipset.ItemsSource = dbContext.Chipset.Local.ToObservableCollection();

            if (window.ShowDialog() == true)
            {
                dbContext.Motherboard.Add(motherboard);
                dbContext.SaveChanges();
            }
        }

        private RelayCommand _remove;
        public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);

        private void PerformRemove(object? commandParameter)
        {
            if (commandParameter is Motherboard motherboard)
            {
                var result = MessageBox.Show($"Удалить данные по {motherboard.Model}", "Предупреждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    motherboard.M2Slots.Clear();
                    dbContext.Motherboard.Remove(motherboard);
                    dbContext.SaveChanges();
                }
            }
        }

        private RelayCommand _edit;
        public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);

        private void PerformEdit(object? commandParameter)
        {
            if (commandParameter is Motherboard motherboard)
            {
                Motherboard motherboardCopy = motherboard.Clone();
                NewMotherboardViewmodel motherboardViewmodel = new(motherboardCopy);
                NewMotherboardWindow window = new NewMotherboardWindow
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Application.Current.MainWindow,
                    DataContext = motherboardViewmodel
                };

                dbContext.Socket.Load();
                dbContext.Chipset.Load();

                window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();
                window.comboBoxChipset.ItemsSource = dbContext.Chipset.Local.ToObservableCollection();

                if (window.ShowDialog() == true)
                {
                    motherboard.Model = motherboardCopy.Model;
                    motherboard.Chipset = motherboardCopy.Chipset;
                    motherboard.RamType = motherboardCopy.RamType;
                    motherboard.MaxRamCapacity = motherboardCopy.MaxRamCapacity;
                    motherboard.Socket = motherboardCopy.Socket;
                    motherboard.M2Slots = motherboardCopy.M2Slots;
                    motherboard.Sata3Ports = motherboardCopy.Sata3Ports;
                    motherboard.PCIex16Slots = motherboardCopy.PCIex16Slots;
                    dbContext.SaveChanges();
                    ViewSource.Refresh();
                }
            }
        }
    }
}

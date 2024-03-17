using Microsoft.EntityFrameworkCore;
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
    internal class CpuViewModel : BaseViewModel
    {
        private readonly ApplicationContext dbContext = new ApplicationContext();

        private readonly CollectionViewSource _viewSource = new CollectionViewSource();

        public ICollectionView ViewSource { get => _viewSource.View; }

        public CpuViewModel()
        {
            dbContext.Cpu.Load();
            _viewSource.Source = dbContext.Cpu.Local.ToObservableCollection();
        }


        private RelayCommand _add;
        public ICommand Add => _add ??= new RelayCommand(PerformAdd);
        private async void PerformAdd(object? commandParameter)
        {
            Cpu cpu = new Cpu();

            NewCpuWindow window = new NewCpuWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = cpu
            };

            await dbContext.Socket.LoadAsync();

            window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

            if (window.ShowDialog() == true)
            {
                await dbContext.Cpu.AddAsync(cpu);
                await dbContext.SaveChangesAsync();
            }
        }

        private RelayCommand _remove;
        public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
        private async void PerformRemove(object? commandParameter)
        {
            if (commandParameter is Cpu cpu)
            {
                var result = MessageBox.Show($"Удалить данные по {cpu.Model}", "Предупреждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    dbContext.Cpu.Remove(cpu);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private RelayCommand _edit;
        public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
        private async void PerformEdit(object? commandParameter)
        {
            if (commandParameter is Cpu cpu)
            {
                Cpu cpuCopy = cpu.Clone();
                NewCpuWindow window = new NewCpuWindow
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Application.Current.MainWindow,
                    DataContext = cpuCopy
                };

                await dbContext.Socket.LoadAsync();

                window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();

                if (window.ShowDialog() == true)
                {
                    dbContext.Cpu.Entry(cpu).CurrentValues.SetValues(cpuCopy);
                    cpu.Socket = cpuCopy.Socket;
                    await dbContext.SaveChangesAsync();
                    ViewSource.Refresh();
                }
            }
        }
    }
}

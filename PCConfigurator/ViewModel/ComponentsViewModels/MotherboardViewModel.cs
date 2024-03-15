using Microsoft.EntityFrameworkCore;
using PCConfigurator.Commands;
using PCConfigurator.Model;
using PCConfigurator.Model.Components;
using PCConfigurator.View.AddComponents;
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

        private RelayCommand remove;
        public ICommand Remove => remove ??= new RelayCommand(PerformRemove);

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
                    _viewSource.View.Refresh();
                }
            }
        }

        private RelayCommand add;
        public ICommand Add => add ??= new RelayCommand(PerformAdd);
       
        private void PerformAdd(object? commandParameter)
        {
            NewMotherboardWindow window = new NewMotherboardWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = new Motherboard()
            };
            window.comboBoxSocket.ItemsSource = dbContext.Socket.Local.ToObservableCollection();
            window.comboBoxChipset.ItemsSource = dbContext.Chipset.Local.ToObservableCollection();

            if (window.ShowDialog() == true)
            {
                
            }
        }
    }
}

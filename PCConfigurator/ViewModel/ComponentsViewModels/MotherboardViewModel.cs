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

public class MotherboardViewModel : ComponentViewModel
{
    public MotherboardViewModel()
    {
        dbContext.Motherboard.Load();
        _viewSource.Source = dbContext.Motherboard.Local.ToObservableCollection();
    }

    protected override void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.Motherboard.Load();
        _viewSource.Source = dbContext.Motherboard.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    protected override void PerformAdd(object? commandParameter)
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

        if (window.ShowDialog() == true)
        {
            dbContext.Motherboard.Add(motherboard);
            dbContext.SaveChanges();
        }
    }

    protected override void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Motherboard motherboard)
        {
            var result = MessageBox.Show($"Удалить данные по {motherboard.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (motherboard.Configurations.Count > 0)
                    MessageBox.Show("Нельзя удалить комплектующее, так как оно используется в конфигурациях.", "Ошибка");
                else
                {
                    dbContext.Motherboard.Remove(motherboard);
                    dbContext.SaveChanges();
                }
            }
        }
    }

    protected override void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Motherboard motherboard)
        {
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

            int i = 0;
            bool[] beforeChanges = new bool[motherboard.Configurations.Count];
            foreach (var configuration in motherboard.Configurations)
            {
                if (configuration.CheckCompatibility().Length > 0)
                    beforeChanges[i++] = true;
                else
                    beforeChanges[i++] = false;
            }

            if (window.ShowDialog() == true)
            {
                if (motherboardViewmodel.RemovedM2Slots.Any(slot => slot.ConfigurationM2Ssds.Count > 0))
                    MessageBox.Show(Application.Current.MainWindow, "Неудалось обновить информацию о комплектующем, " +
                        "так как при этом нарушалась бы целостность конфигураций.", "Ошибка");
                else
                {
                    bool[] afterChanges = new bool[motherboard.Configurations.Count];
                    i = 0;
                    foreach (var configuration in motherboard.Configurations)
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
            }

            ResetContext();
        }
    }
}
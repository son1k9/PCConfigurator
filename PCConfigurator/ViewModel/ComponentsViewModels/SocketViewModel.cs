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

public class SocketViewModel : ComponentViewModel
{
    public SocketViewModel()
    {
        dbContext.Socket.Load();
        _viewSource.Source = dbContext.Socket.Local.ToObservableCollection();
    }

    protected override void ResetContext()
    {
        dbContext.Dispose();
        dbContext = new ApplicationContext();
        dbContext.Socket.Load();
        _viewSource.Source = dbContext.Socket.Local.ToObservableCollection();
        OnPropertyChanged(nameof(ViewSource));
    }

    protected override void PerformAdd(object? commandParameter)
    {
        Socket socket = new Socket();
        NewSocketViewModel viewModel = new NewSocketViewModel(socket);

        NewSocketWindow window = new NewSocketWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            DataContext = viewModel
        };

        if (window.ShowDialog() == true)
        {
            dbContext.Socket.Add(socket);
            dbContext.SaveChanges();
        }
    }

    protected override void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Socket socket)
        {
            var result = MessageBox.Show($"Удалить данные по {socket.Name}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (socket.Cpus.Count > 0 || socket.Coolers.Count > 0 || socket.Motherboards.Count > 0)
                {
                    MessageBox.Show("Нельзя удалить сокет, так как он используется в комплектующих.", "Ошибка");
                    return;
                }

                dbContext.Socket.Remove(socket);
                dbContext.SaveChanges();
            }
        }
    }

    protected override void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Socket socket)
        {
            NewSocketViewModel viewModel = new NewSocketViewModel(socket);
            NewSocketWindow window = new NewSocketWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = viewModel
            };

            dbContext.Chipset.Load();

            if (window.ShowDialog() == true)
            {
                if (viewModel.RemovedChipsets.Any(slot => slot.Motherboards.Count > 0))
                        MessageBox.Show("Нельзя удалить чипсет, который используется в материнской плате.", "Ошибка");
                else
                    dbContext.SaveChanges();
            }
            ResetContext();
        }
    }
}

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

internal class SocketViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public SocketViewModel()
    {
        dbContext.Socket.Load();
        _viewSource.Source = dbContext.Socket.Local.ToObservableCollection();
    }

    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private void PerformAdd(object? commandParameter)
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

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Socket socket)
        {
            var result = MessageBox.Show($"Удалить данные по {socket.Name}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (socket.Cpus.Count > 0 || socket.Coolers.Count > 0 || socket.Motherboards.Count > 0)
                {
                    MessageBox.Show("Нельзя удалить сокет, который используется в других комплектующих.", "Ошибка");
                    return;
                }

                dbContext.Socket.Remove(socket);
                dbContext.SaveChanges();
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Socket socket)
        {
            Socket socketCopy = socket.Clone();
            NewSocketViewModel viewModel = new NewSocketViewModel(socketCopy);
            NewSocketWindow window = new NewSocketWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = viewModel
            };

            dbContext.Chipset.Load();

            if (window.ShowDialog() == true)
            {
                var deletedChipsets = (from n in socket.Chipsets
                                       where !socketCopy.Chipsets.Contains(n)
                                       select n).ToList();
                foreach (var chipset in deletedChipsets)
                {
                    if (chipset.Motherboards.Count > 0)
                    {
                        MessageBox.Show("Нельзя удалить чипсет, который используется в других комплектующих.", "Ошибка");
                        return;
                    }

                }

                dbContext.Socket.Entry(socket).CurrentValues.SetValues(socketCopy);
                socket.Chipsets = socketCopy.Chipsets;
                dbContext.SaveChanges();
                ViewSource.Refresh();
            }
        }
    }
}

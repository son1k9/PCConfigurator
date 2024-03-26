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

internal class GpuViewModel : BaseViewModel
{
    private readonly ApplicationContext dbContext = new ApplicationContext();

    private readonly CollectionViewSource _viewSource = new CollectionViewSource();

    public ICollectionView ViewSource { get => _viewSource.View; }

    public GpuViewModel()
    {
        dbContext.Gpu.Load();
        _viewSource.Source = dbContext.Gpu.Local.ToObservableCollection();
    }

    private RelayCommand _add;
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    private async void PerformAdd(object? commandParameter)
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
            await dbContext.Gpu.AddAsync(gpu);
            await dbContext.SaveChangesAsync();
        }
    }

    private RelayCommand _remove;
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    private async void PerformRemove(object? commandParameter)
    {
        if (commandParameter is Gpu gpu)
        {
            var result = MessageBox.Show($"Удалить данные по {gpu.Model}", "Предупреждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbContext.Gpu.Remove(gpu);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private RelayCommand _edit;
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    private async void PerformEdit(object? commandParameter)
    {
        if (commandParameter is Gpu gpu)
        {
            Gpu gpuCopy = gpu.Clone();
            NewGpuWindow window = new NewGpuWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                DataContext = gpuCopy
            };

            if (window.ShowDialog() == true)
            {
                dbContext.Gpu.Entry(gpu).CurrentValues.SetValues(gpuCopy);
                await dbContext.SaveChangesAsync();
                ViewSource.Refresh();
            }
        }
    }
}

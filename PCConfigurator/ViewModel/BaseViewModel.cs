using System.ComponentModel;

namespace PCConfigurator.ViewModel;
/// <summary>
/// Base class for all ViewModels
/// </summary>
internal abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raises PropertyChanged event with a given property
    /// </summary>
    /// <param name="propertyName">Property name</param>
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

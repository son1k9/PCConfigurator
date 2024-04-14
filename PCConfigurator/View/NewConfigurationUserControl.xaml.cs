using PCConfigurator.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCConfigurator.View
{
    /// <summary>
    /// Interaction logic for NewConfigurationUserControl.xaml
    /// </summary>
    public partial class NewConfigurationUserControl : UserControl, INotifyPropertyChanged
    {
        ConfigurationViewModel? _viewmodel;

        public NewConfigurationUserControl()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public SolidColorBrush Color 
        {
            get 
            {
                if (_viewmodel?.Errors.Length > 0)
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F55762"));
                else
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#76E276"));
            } 
        }

        public string ErrorsListHeader
        {
            get
            {
                if (_viewmodel?.Errors.Length > 0)
                {
                    string error = string.Empty;
                    int lenght = _viewmodel.Errors.Length;
                    if (lenght == 1 || lenght % 10 == 1)
                        error = "Ошибка";
                    else if (lenght < 5 || lenght % 10 < 5)
                        error = "Ошибки";
                    else
                        error = "Ошибок";

                    return $"{_viewmodel.Errors.Length} {error}";
                }
                else
                    return "Ошибки не найдены";
            }
        }

        private void Refresh(object? sender, object array)
        {
            if (array == null)
                RefreshAll();
            else
                RefreshView(array);
        }

        private void RefreshView(object collection)
        {
            CollectionViewSource.GetDefaultView(collection).Refresh();
        }

        private void RefreshAll()
        {
            RefreshView(CollectionViewSource.GetDefaultView(RamsItemControl.ItemsSource));
            RefreshView(CollectionViewSource.GetDefaultView(GpusItemControl.ItemsSource));
            RefreshView(CollectionViewSource.GetDefaultView(M2SsdsItemControl.ItemsSource));
            RefreshView(CollectionViewSource.GetDefaultView(SataItemControl.ItemsSource));
        }

        private void configuration_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is ConfigurationViewModel viewModel)
            {
                _viewmodel = viewModel;
                _viewmodel.ArrayChanged += Refresh;
                _viewmodel.PropertyChanged += (sender, e) => 
                {
                    if (e.PropertyName == "Errors")
                    {
                        OnPropertyChanged(nameof(Color));
                        OnPropertyChanged(nameof(ErrorsListHeader));
                    }
                };
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(ErrorsListHeader));
            }
        }
    }
}

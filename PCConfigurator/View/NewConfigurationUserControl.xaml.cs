using PCConfigurator.ViewModel;
using System;
using System.Collections.Generic;
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
    public partial class NewConfigurationUserControl : UserControl
    {
        private ConfigurationViewModel _viewModel;

        public NewConfigurationUserControl()
        {
            InitializeComponent();
        }

        private void Refresh(object sender, object collection)
        {
            var view = CollectionViewSource.GetDefaultView(RamItemsControl.ItemsSource);
            view.Refresh();
        }

        private void configuration_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ConfigurationViewModel viewModel)
            {
                _viewModel = viewModel;
                viewModel.ComponentChanged += Refresh;
            }
        }

        private void configuration_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is ConfigurationViewModel viewModel)
            {
                _viewModel = viewModel;
                viewModel.ComponentChanged += Refresh;
            }
        }
    }
}

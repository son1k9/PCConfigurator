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
        public NewConfigurationUserControl()
        {
            InitializeComponent();
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
                viewModel.ArrayChanged += Refresh;
            }
        }
    }
}

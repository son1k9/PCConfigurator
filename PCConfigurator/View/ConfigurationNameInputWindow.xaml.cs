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
using System.Windows.Shapes;

namespace PCConfigurator.View
{
    /// <summary>
    /// Interaction logic for ConfigurationNameInputWindow.xaml
    /// </summary>
    public partial class ConfigurationNameInputWindow : Window
    {
        public ConfigurationNameInputWindow()
        {
            InitializeComponent();
        }

        private void AnnounceError(string message)
        {
            textBoxError.Visibility = Visibility.Visible;
            textBoxError.Text = message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text.Length == 0) 
            {
                AnnounceError("Введите название.");
                return;
            }
            DialogResult = true;
        }
    }
}

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

namespace PCConfigurator.View.AddComponents
{
    /// <summary>
    /// Interaction logic for NewM2SsdWindow.xaml
    /// </summary>
    public partial class NewM2SsdWindow : Window
    {
        public NewM2SsdWindow()
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
            if (textBoxModel.Text.Length == 0)
            {
                AnnounceError("Введите модель.");
                return;
            }
            if (comboBoxNandType.SelectedItem is null)
            {
                AnnounceError("Выберите тип памяти.");
                return;
            }

            DialogResult = true;
        }
    }
}

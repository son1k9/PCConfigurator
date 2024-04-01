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

        private void CheckBoxTb_Checked(object sender, RoutedEventArgs e)
        {
            sliderSsdCapacity.Minimum = 1;
            sliderSsdCapacity.Maximum = 100;
            sliderSsdCapacity.TickFrequency = 1;
            sliderSsdCapacity.Value = sliderSsdCapacity.Value;
        }

        private void CheckBoxTb_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderSsdCapacity.Minimum = 60;
            sliderSsdCapacity.Maximum = 1024;
            sliderSsdCapacity.TickFrequency = 2;
            sliderSsdCapacity.Value = 1000;
        }
    }
}

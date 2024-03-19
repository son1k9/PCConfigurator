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
    /// Interaction logic for NewRamWindow.xaml
    /// </summary>
    public partial class NewRamWindow : Window
    {
        public NewRamWindow()
        {
            InitializeComponent();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void comboBoxRamType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            sliderRamClock.Value = sliderRamClock.Value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sliderRamClock.Value = sliderRamClock.Minimum;
        }
    }
}

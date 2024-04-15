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
    /// Interaction logic for AddConfiguartionWindow.xaml
    /// </summary>
    public partial class AddConfiguartionWindow : Window
    {
        public bool FromFile { get; set; } = false;

        public AddConfiguartionWindow()
        {
            InitializeComponent();
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            FromFile = false;
            DialogResult = true;
        }

        private void ButtonFromFile_Click(object sender, RoutedEventArgs e)
        {
            FromFile = true;
            DialogResult = true;
        }
    }
}

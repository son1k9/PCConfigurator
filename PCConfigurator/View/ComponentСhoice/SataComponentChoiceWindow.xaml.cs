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

namespace PCConfigurator.View.ComponentСhoice
{
    /// <summary>
    /// Interaction logic for SataComponentChoiceWindow.xaml
    /// </summary>
    public partial class SataComponentChoiceWindow : Window
    {
        public SataComponentChoiceWindow()
        {
            InitializeComponent();
        }

        public enum Storage
        {
            HDD,
            SSD
        }

        public Storage Clicked { get; set; }

        void ButtonHdd_Click(object sender, RoutedEventArgs e)
        {
            Clicked = Storage.HDD;
            DialogResult = true;
        }

        void ButtonSsd_Click(object sender, RoutedEventArgs e)
        {
            Clicked = Storage.SSD;
            DialogResult = true;
        }
    }
}

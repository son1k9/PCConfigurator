using System.Windows;

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

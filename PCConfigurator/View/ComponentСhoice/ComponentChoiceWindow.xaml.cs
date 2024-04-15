using System.Windows;

namespace PCConfigurator.View.ComponentСhoice
{
    /// <summary>
    /// Interaction logic for MotherboardChoiceUserControl.xaml
    /// </summary>
    public partial class ComponentChoiceWindow : Window
    {
        public ComponentChoiceWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

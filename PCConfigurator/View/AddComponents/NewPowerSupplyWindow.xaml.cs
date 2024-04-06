using System.Windows;

namespace PCConfigurator.View.AddComponents
{
    /// <summary>
    /// Interaction logic for NewPowerSupplyWindow.xaml
    /// </summary>
    public partial class NewPowerSupplyWindow : Window
    {
        public NewPowerSupplyWindow()
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

            DialogResult = true;
        }
    }
}

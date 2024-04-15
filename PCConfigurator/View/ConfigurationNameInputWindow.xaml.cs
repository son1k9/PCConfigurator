using System.Windows;

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

using System.Windows;

namespace PCConfigurator.View.AddComponents
{
    /// <summary>
    /// Interaction logic for NewSocketWindow.xaml
    /// </summary>
    public partial class NewSocketWindow : Window
    {
        public NewSocketWindow()
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
                AnnounceError("Введите сокет.");
                return;
            }
            if (listBoxChipsets.Items.Count == 0)
            {
                AnnounceError("Укажите чипсет.");
                return;
            }

            DialogResult = true;
        }
    }
}

using System.Windows;

namespace PCConfigurator.View.AddComponents;

/// <summary>
/// Interaction logic for NewCoolerWindow.xaml
/// </summary>
public partial class NewCoolerWindow : Window
{
    public NewCoolerWindow()
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
        if (listBoxSocket.Items.Count == 0)
        {
            AnnounceError("Выберите сокет.");
            return;
        }

        DialogResult = true;
    }
}

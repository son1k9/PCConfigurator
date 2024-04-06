using System.Windows;
using System.Windows.Controls;

namespace PCConfigurator.View.AddComponents;

/// <summary>
/// Interaction logic for NewRamWindow.xaml
/// </summary>
public partial class NewRamWindow : Window
{
    public NewRamWindow()
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
        if (comboBoxRamType.SelectedItem is null)
        {
            AnnounceError("Выберите тип памяти.");
            return;
        }

        DialogResult = true;
    }

    private void comboBoxRamType_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        sliderRamClock.Value = sliderRamClock.Value;
        sliderRamCl.Value = sliderRamCl.Value;
    }
}

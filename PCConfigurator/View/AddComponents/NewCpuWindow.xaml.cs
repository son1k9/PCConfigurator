using System.Windows;

namespace PCConfigurator.View.AddComponents;

/// <summary>
/// Interaction logic for NewCpuWindow.xaml
/// </summary>
public partial class NewCpuWindow : Window
{
    public NewCpuWindow()
    {
        InitializeComponent();
    }

    private void sliderCoreClock_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (sliderCoreClock.Value > sliderBoostClock?.Value)
        {
            sliderBoostClock.Value = sliderCoreClock.Value;
        }
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
        if (comboBoxSocket.SelectedItem == null)
        {
            AnnounceError("Выберите сокет.");
            return;
        }
        if (!(checkBoxDDR3.IsChecked.GetValueOrDefault()
            | checkBoxDDR4.IsChecked.GetValueOrDefault() | checkBoxDDR5.IsChecked.GetValueOrDefault()))
        {
            AnnounceError("Выберите тип памяти.");
            return;
        }


        DialogResult = true;
    }
}

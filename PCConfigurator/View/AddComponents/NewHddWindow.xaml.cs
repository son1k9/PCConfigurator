using System.Windows;

namespace PCConfigurator.View.AddComponents
{
    /// <summary>
    /// Interaction logic for NewHddWindow.xaml
    /// </summary>
    public partial class NewHddWindow : Window
    {
        public NewHddWindow()
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

        private void CheckBoxTb_Checked(object sender, RoutedEventArgs e)
        {
            sliderCapacity.Minimum = 1;
            sliderCapacity.Maximum = 22;
            sliderCapacity.TickFrequency = 1;
            sliderCapacity.Value = sliderCapacity.Value;
        }

        private void CheckBoxTb_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderCapacity.Minimum = 60;
            sliderCapacity.Maximum = 1000;
            sliderCapacity.TickFrequency = 2;
            sliderCapacity.Value = 1000;
        }
    }
}

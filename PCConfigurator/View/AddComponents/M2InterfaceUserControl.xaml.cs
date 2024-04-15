using System.Windows.Controls;
using System.Windows.Media;

namespace PCConfigurator.View.AddComponents
{
    /// <summary>
    /// Interaction logic for M2InterfaceUserControl.xaml
    /// </summary>
    public partial class M2InterfaceUserControl : UserControl
    {
        public M2InterfaceUserControl()
        {
            InitializeComponent();
            CompositionTarget.Rendering += UserControl_Loaded;
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            if (CheckBoxNvme.IsChecked.Value)
            {
                if (CheckBoxSata.IsChecked.Value)
                {
                    CheckBoxNvme.IsEnabled = true;
                    CheckBoxSata.IsEnabled = true;
                }
                else
                {
                    CheckBoxNvme.IsEnabled = false;
                    CheckBoxSata.IsEnabled = true;
                }
            }
            else if (CheckBoxSata.IsChecked.Value)
            {
                CheckBoxNvme.IsEnabled = true;
                CheckBoxSata.IsEnabled = false;
            }
        }

        private void UserControl_Loaded(object sender, EventArgs e)
        {
            CheckBox_Click(this, new EventArgs());
        }
    }
}

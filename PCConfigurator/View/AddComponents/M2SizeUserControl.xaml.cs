using System.Windows.Controls;
using System.Windows.Media;

namespace PCConfigurator.View.AddComponents
{
    /// <summary>
    /// Interaction logic for M2SizeUserControl.xaml
    /// </summary>
    public partial class M2SizeUserControl : UserControl
    {
        public M2SizeUserControl()
        {
            InitializeComponent();
            CompositionTarget.Rendering += UserControl_Loaded;
        }

        private void CheckBoxSize_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (var child in StackPanelSize.Children)
            {
                if (child is CheckBox checkBox)
                {
                    if (checkBox.IsChecked.Value)
                        i++;

                    checkBox.IsEnabled = true;
                }
            }

            if (i == 1)
            {
                foreach (var child in StackPanelSize.Children)
                {
                    if (child is CheckBox checkBox)
                    {
                        if (checkBox.IsChecked.Value)
                            checkBox.IsEnabled = false;
                        else
                            checkBox.IsEnabled = true;
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, EventArgs e)
        {
            CheckBoxSize_Click(this, new EventArgs());
        }
    }
}

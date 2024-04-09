using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

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

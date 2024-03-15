using PCConfigurator.Converters;
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

namespace PCConfigurator.View.ComponentsUserControls
{
    /// <summary>
    /// Interaction logic for MotherboardUserControl.xaml
    /// </summary>
    public partial class MotherboardUserControl : UserControl
    {
        public MotherboardUserControl()
        {
            InitializeComponent();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "M2Slots")
            {
                if (e.Column is DataGridTextColumn textColumn) 
                {
                    if (textColumn.Binding is Binding binding) 
                    {
                        binding.Converter = new M2SlotsConverter();
                    }
                }
            }

            if (e.PropertyName == "MotherboardId")
                e.Cancel = true;

            if (e.PropertyName == "Configurations")
                e.Cancel = true;

            if (e.PropertyName == "SocketId")
                e.Cancel = true;

            if (e.PropertyName == "ChipsetId")
                e.Cancel = true;
        }
    }
}

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
    /// Interaction logic for SocketUserControl.xaml
    /// </summary>
    public partial class SocketUserControl : UserControl
    {
        public SocketUserControl()
        {
            InitializeComponent();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "SocketId")
                e.Cancel = true;

            else if (e.PropertyName == "Coolers")
                e.Cancel = true;

            else if (e.PropertyName == "Cpus")
                e.Cancel = true;

            else if (e.PropertyName == "Motherboards")
                e.Cancel = true;

            else if (e.PropertyName == "Name")
                e.Column.Header = "Сокет";

            else if (e.PropertyName == "Chipsets")
            {
                e.Column.Header = "Чипсет";
                if (e.Column is DataGridTextColumn textColumn)
                {
                    if (textColumn.Binding is Binding binding)
                    {
                        binding.Converter = new ChipsetsConverter();
                    }
                }
            }
        }
    }
}

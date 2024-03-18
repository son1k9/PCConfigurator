using PCConfigurator.Converters;
using System.Windows.Controls;
using System.Windows.Data;

namespace PCConfigurator.View.ComponentsUserControls;

/// <summary>
/// Interaction logic for CoolerViewModel.xaml
/// </summary>
public partial class CoolerUserControl : UserControl
{
    public CoolerUserControl()
    {
        InitializeComponent();
    }

    private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyName == "CoolerId")
            e.Cancel = true;

        else if (e.PropertyName == "Configurations")
            e.Cancel = true;

        else if (e.PropertyName == "Sockets")
        {
            e.Column.Header = "Сокет";
            if (e.Column is DataGridTextColumn textColumn)
            {
                if (textColumn.Binding is Binding binding)
                {
                    binding.Converter = new SocketsConverter();
                }
            }
        }

        else if (e.PropertyName == "Model")
            e.Column.Header = "Модель";

        else if (e.PropertyName == "Tdp")
            e.Column.Header = "Рассеиваемая мощность";

        else if (e.PropertyName == "Rpm")
            e.Column.Header = "Максимальная скорость вращения";
    }
}

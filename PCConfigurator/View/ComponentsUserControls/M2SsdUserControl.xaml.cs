using PCConfigurator.Converters;
using System.Windows.Controls;
using System.Windows.Data;

namespace PCConfigurator.View.ComponentsUserControls;

/// <summary>
/// Interaction logic for M2SsdUserControl.xaml
/// </summary>
public partial class M2SsdUserControl : UserControl
{
    public M2SsdUserControl()
    {
        InitializeComponent();
    }

    private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyName == "M2SsdId")
            e.Cancel = true;

        else if (e.PropertyName == "ConfigurationM2Ssds")
            e.Cancel = true;

        else if (e.PropertyName == "IsNvme")
            e.Cancel = true;

        else if (e.PropertyName == "IsSata")
            e.Cancel = true;

        else if (e.PropertyName == "Is2230")
            e.Cancel = true;

        else if (e.PropertyName == "Is2242")
            e.Cancel = true;

        else if (e.PropertyName == "Is2260")
            e.Cancel = true;

        else if (e.PropertyName == "Is2280")
            e.Cancel = true;

        else if (e.PropertyName == "Is22110")
            e.Cancel = true;

        else if (e.PropertyName == "Capacity")
            e.Column.Header = "Объем";

        else if (e.PropertyName == "ReadSpeed")
            e.Column.Header = "Скорость чтения";

        else if (e.PropertyName == "WriteSpeed")
            e.Column.Header = "Скорость записи";

        else if (e.PropertyName == "Tbw")
            e.Column.Header = "Максимальный ресурс записи";

        else if (e.PropertyName == "NandType")
            e.Column.Header = "Тип памяти";

        else if (e.PropertyName == "Model")
        {
            e.Column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Auto);
            e.Column.Header = "Модель";
        }

        else if (e.PropertyName == "M2Interface")
        {
            DataGridTextColumn column = new DataGridTextColumn()
            {
                Header = "Интерфейс"
            };
            Binding binding = new Binding(e.PropertyName);
            column.Binding = binding;
            e.Column = column;
            column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Auto);
        }

        else if (e.PropertyName == "M2Size")
        {
            DataGridTextColumn column = new DataGridTextColumn()
            {
                Header = "Размер"
            };
            Binding binding = new Binding(e.PropertyName)
            {
                Converter = new M2SizeConverter()
            };
            column.Binding = binding;
            e.Column = column;
            column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Auto);
        }
    }
}

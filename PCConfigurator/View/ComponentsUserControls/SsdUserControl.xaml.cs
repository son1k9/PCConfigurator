using PCConfigurator.Model.Components;
using System.Windows.Controls;

namespace PCConfigurator.View.ComponentsUserControls;

/// <summary>
/// Interaction logic for SsdUserControl.xaml
/// </summary>
public partial class SsdUserControl : UserControl
{
    public SsdUserControl()
    {
        InitializeComponent();
    }

    private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyName == "SsdId")
            e.Cancel = true;

        else if (e.PropertyName == "Configurations")
            e.Cancel = true;

        else if (e.PropertyName == "Model")
        {
            e.Column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Auto);
            e.Column.Header = "Модель";
        }

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
    }
}

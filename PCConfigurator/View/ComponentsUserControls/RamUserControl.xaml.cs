using System.Windows.Controls;

namespace PCConfigurator.View.ComponentsUserControls;

/// <summary>
/// Interaction logic for RamUserControl.xaml
/// </summary>
public partial class RamUserControl : UserControl
{
    public RamUserControl()
    {
        InitializeComponent();
    }

    private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyName == "RamID")
            e.Cancel = true;

        else if (e.PropertyName == "Configurations")
            e.Cancel = true;

        else if (e.PropertyName == "Model")
            e.Column.Header = "Модель";

        else if (e.PropertyName == "RamType")
            e.Column.Header = "Тип";

        else if (e.PropertyName == "Capacity")
            e.Column.Header = "Объем";

        else if (e.PropertyName == "Clock")
            e.Column.Header = "Частота";

        else if (e.PropertyName == "Cl")
            e.Column.Header = "CL";
    }
}

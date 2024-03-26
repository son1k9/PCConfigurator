using System.Windows.Controls;

namespace PCConfigurator.View.ComponentsUserControls;

/// <summary>
/// Interaction logic for PowerSupplyUserControl.xaml
/// </summary>
public partial class PowerSupplyUserControl : UserControl
{
    public PowerSupplyUserControl()
    {
        InitializeComponent();
    }

    private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyName == "PowerSupplyId")
            e.Cancel = true;

        else if (e.PropertyName == "Configurations")
            e.Cancel = true;

        else if (e.PropertyName == "Model")
            e.Column.Header = "Модель";

        else if (e.PropertyName == "Wattage")
            e.Column.Header = "Мощность";
    }
}

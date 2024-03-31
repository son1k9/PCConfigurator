using System.Windows.Controls;

namespace PCConfigurator.View.ComponentsUserControls;

/// <summary>
/// Interaction logic for HddUserControl.xaml
/// </summary>
public partial class HddUserControl : UserControl
{
    public HddUserControl()
    {
        InitializeComponent();
    }

    private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyName == "HddId")
            e.Cancel = true;

        else if (e.PropertyName == "Configurations")
            e.Cancel = true;

        else if (e.PropertyName == "Model")
            e.Column.Header = "Модель";

        else if (e.PropertyName == "Capacity")
            e.Column.Header = "Объем";

        else if (e.PropertyName == "SpindelSpeed")
            e.Column.Header = "Скорость вращения шпинделя";
    }
}

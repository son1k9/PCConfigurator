using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows.Controls;
using System.Windows.Data;

namespace PCConfigurator.View.ComponentsUserControls;

/// <summary>
/// Interaction logic for CpuUserControl.xaml
/// </summary>
public partial class CpuUserControl : UserControl
{
    public CpuUserControl()
    {
        InitializeComponent();
    }

    private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyName == "CpuId")
            e.Cancel = true;

        else if (e.PropertyName == "SocketId")
            e.Cancel = true;

        else if (e.PropertyName == "Configurations")
            e.Cancel = true;

        else if (e.PropertyName == "IsDDR")
            e.Cancel = true;

        else if (e.PropertyName == "IsDDR2")
            e.Cancel = true;

        else if (e.PropertyName == "IsDDR3")
            e.Cancel = true;

        else if (e.PropertyName == "IsDDR4")
            e.Cancel = true;

        else if (e.PropertyName == "IsDDR5")
            e.Cancel = true;

        else if (e.PropertyName == "RamTypes")
        {
            DataGridTextColumn column = new DataGridTextColumn()
            {
                Header = "Тип памяти"
            };
            Binding binding = new Binding(e.PropertyName);
            column.Binding = binding;
            e.Column = column;
            column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Auto);
        }

        else if (e.PropertyName == "Model")
        {
            e.Column.Header = "Модель";
            e.Column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Auto);
        }

        else if (e.PropertyName == "Socket")
            e.Column.Header = "Сокет";

        else if (e.PropertyName == "Cores")
            e.Column.Header = "Ядра";

        else if (e.PropertyName == "ECores")
            e.Column.Header = "Энергоэффективные ядра";

        else if (e.PropertyName == "Smt")
            e.Column.Header = "SMT";

        else if (e.PropertyName == "CoreClock")
            e.Column.Header = "Базовая частота";

        else if (e.PropertyName == "BoostClock")
            e.Column.Header = "Частота в турбо режиме";

        else if (e.PropertyName == "L2Cache")
            e.Column.Header = "Объём кэша L2";

        else if (e.PropertyName == "L3Cache")
            e.Column.Header = "Объём кэша L3";

        else if (e.PropertyName == "Tdp")
            e.Column.Header = "TDP";

        else if (e.PropertyName == "MaxRamCapacity")
            e.Column.Header = "Максимальный объем памяти";

        else if (e.PropertyName == "HaveGraphics")
            e.Column.Header = "Графическое ядро";
    }
}

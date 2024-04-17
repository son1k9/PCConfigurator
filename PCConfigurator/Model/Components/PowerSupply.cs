namespace PCConfigurator.Model.Components;

public class PowerSupply : Component
{
    public int PowerSupplyId { get; set; }

    public int Wattage { get; set; } = 200;

    public virtual List<Configuration> Configurations { get; set; } = [];
}

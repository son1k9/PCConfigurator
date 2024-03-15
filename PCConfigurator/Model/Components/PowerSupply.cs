namespace PCConfigurator.Model.Components;

public class PowerSupply
{
    public int PowerSupplyId { get; set; }
    public required string Model { get; set; }
    public required int Wattage { get; set; }

    public virtual List<Configuration> Configurations { get; } = [];
}

namespace PCConfigurator.Model.Components;

internal class PowerSupply
{
    public int PowerSupplyId { get; set; }
    public required string Model { get; set; }
    public required int Wattage { get; set; }

    public List<Configuration> Configurations { get; } = [];
}

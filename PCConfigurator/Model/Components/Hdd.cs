namespace PCConfigurator.Model.Components;

public class Hdd
{
    public int HddId { get; set; }
    public required string Model { get; set; }
    public required int Capacity { get; set; }
    public required int SpindelSpeed { get; set; }

    public virtual List<Configuration> Configurations { get; } = [];
}

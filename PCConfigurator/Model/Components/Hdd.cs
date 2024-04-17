namespace PCConfigurator.Model.Components;

public class Hdd : Component
{
    public int HddId { get; set; }

    public int Capacity { get; set; } = 80;

    public int SpindelSpeed { get; set; } = 4200;

    public virtual List<Configuration> Configurations { get; set; } = [];
    public virtual List<ConfigurationHdd> ConfigurationHdds { get; set; } = [];
}

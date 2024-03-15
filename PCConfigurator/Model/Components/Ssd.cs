namespace PCConfigurator.Model.Components;

public class Ssd
{
    public int SsdId { get; set; }
    public required string Model { get; set; }
    public required int Capacity { get; set; }
    public required int ReadSpeed { get; set; }
    public required int WriteSpeed { get; set; }
    public required int Tbw { get; set; }
    public required NandType NandType { get; set; }

    public virtual List<Configuration> Configurations { get; } = [];
}

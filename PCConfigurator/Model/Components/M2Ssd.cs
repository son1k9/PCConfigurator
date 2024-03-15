namespace PCConfigurator.Model.Components;

public class M2Ssd
{
    public int M2SsdId { get; set; }
    public required string Model { get; set; }
    public required int Capacity { get; set; }
    public required int ReadSpeed { get; set; }
    public required int WriteSpeed { get; set; }
    public required int Tbw { get; set; }
    public required NandType NandType { get; set; }
    public required M2.M2Interface M2Interface { get; set; }
    public required M2.M2Size M2Size { get; set; }

    public virtual List<Configuration> Configurations { get; } = [];
}

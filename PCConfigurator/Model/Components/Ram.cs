namespace PCConfigurator.Model.Components;

internal class Ram
{
    public int RamID { get; set; }
    public required string Model { get; set; }
    public required int Capacity { get; set; }
    public required int Clock { get; set; }
    public required int Cl { get; set; }
    public required RamType RamType { get; set; }

    public List<Configuration> Configurations { get; } = [];
}

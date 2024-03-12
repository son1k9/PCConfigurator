namespace PCConfigurator.Model.Components;

internal class Gpu
{
    public int GpuId { get; set; }
    public required string Model { get; set; }
    public required float CoreClock { get; set; }
    public required float BoostClock { get; set; }
    public required int VramCapacity { get; set; }
    public required float VramClock { get; set; }
    public required int PowerConsumption { get; set; }

    public List<Configuration> Configurations { get; set; } = [];
}

namespace PCConfigurator.Model.Components;

public class Gpu
{
    public int GpuId { get; set; }

    public string Model { get; set; }

    public int CoreClock { get; set; } = 400;

    public int BoostClock { get; set; } = 400;

    public int VramCapacity { get; set; } = 1;

    public int PowerConsumption { get; set; } = 15;

    public virtual List<Configuration> Configurations { get; set; } = [];
    public virtual List<ConfigurationGpu> ConfigurationGpus { get; set; } = [];


    public Gpu Clone()
    {
        return new Gpu()
        {
            GpuId = GpuId,
            Model = Model,
            CoreClock = CoreClock,
            BoostClock = BoostClock,
            VramCapacity = VramCapacity,
            PowerConsumption = PowerConsumption,
            Configurations = Configurations
        };
    }
}

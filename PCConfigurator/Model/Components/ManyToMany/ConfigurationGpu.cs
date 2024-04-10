namespace PCConfigurator.Model.Components;

public class ConfigurationGpu
{
    public int ConfigurationGpuId { get; set; }

    public int ConfigurationId { get; set; }
    public virtual Configuration Configuration { get; set; }

    public int GpuId { get; set; }
    public virtual Gpu Gpu { get; set; }
}

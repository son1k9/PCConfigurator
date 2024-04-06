namespace PCConfigurator.Model.Components;

public class ConfigurationRam
{
    public int ConfigurationRamId { get; set; }

    public int ConfigurationId { get; set; }
    public virtual Configuration Configuration { get; set; }

    public int RamId { get; set; }
    public virtual Ram Ram { get; set; }
}

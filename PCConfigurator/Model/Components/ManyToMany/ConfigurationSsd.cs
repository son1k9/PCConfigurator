namespace PCConfigurator.Model.Components;

public class ConfigurationSsd
{
    public int ConfigurationSsdId { get; set; }

    public int ConfigurationId { get; set; }
    public virtual Configuration Configuration { get; set; }

    public int SsdId { get; set; }
    public virtual Ssd Ssd { get; set; }
}

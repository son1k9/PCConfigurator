namespace PCConfigurator.Model.Components;

public class ConfigurationHdd
{
    public int ConfigurationHddId { get; set; }

    public int ConfigurationId { get; set; }
    public virtual Configuration Configuration { get; set; }

    public int HddId { get; set; }
    public virtual Hdd Hdd { get; set; }
}

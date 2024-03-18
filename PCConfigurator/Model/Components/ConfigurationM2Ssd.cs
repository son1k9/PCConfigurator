namespace PCConfigurator.Model.Components;

public class ConfigurationM2Ssd
{
    public int ConfigurationId { get; set; }
    public virtual Configuration Configuration { get; set; }
    public int M2SsdId { get; set; }
    public virtual M2Ssd M2Ssd { get; set; }
    public int M2SlotId { get; set; }
    public virtual M2Slot M2Slot { get; set; }
}

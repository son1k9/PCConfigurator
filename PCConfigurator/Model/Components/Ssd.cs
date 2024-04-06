namespace PCConfigurator.Model.Components;

public class Ssd
{
    public int SsdId { get; set; }

    public string Model { get; set; }

    public int Capacity { get; set; } = 60;

    public int ReadSpeed { get; set; } = 350;

    public int WriteSpeed { get; set; } = 350;

    public int Tbw { get; set; } = 30;

    public NandType NandType { get; set; }

    public virtual List<Configuration> Configurations { get; set; } = [];
    public virtual List<ConfigurationSsd> ConfigurationSsds { get; set; } = [];

    public Ssd Clone()
    {
        return new Ssd()
        {
            SsdId = SsdId,
            Model = Model,
            Capacity = Capacity,
            ReadSpeed = ReadSpeed,
            WriteSpeed = WriteSpeed,
            Tbw = Tbw,
            NandType = NandType,
            Configurations = Configurations
        };
    }
}

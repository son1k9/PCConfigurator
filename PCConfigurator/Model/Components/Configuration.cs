namespace PCConfigurator.Model.Components;

public class Configuration
{
    public int ConfigurationId { get; set; }

    public string Name { get; set; } = "";

    public int? MotherboardId { get; set; }
    public virtual Motherboard? Motherboard { get; set; }

    public int? CpuId { get; set; }
    public virtual Cpu? Cpu { get; set; }

    public int? CoolerId { get; set; }
    public virtual Cooler? Cooler { get; set; }

    public int? PowerSupplyId { get; set; }
    public virtual PowerSupply? PowerSupply { get; set; }

    public virtual List<Ram> Rams { get; set; } = [];
    public virtual List<ConfigurationRam> ConfigurationRams { get; set; } = [];

    public virtual List<Gpu> Gpus { get; set; } = [];
    public virtual List<ConfigurationGpu> ConfigurationGpus { get; set; } = [];

    public virtual List<Ssd> Ssds { get; set; } = [];
    public virtual List<ConfigurationSsd> ConfigurationSsds { get; set; } = [];

    public virtual List<Hdd> Hdds { get; set; } = [];
    public virtual List<ConfigurationHdd> ConfigurationHdds { get; set; } = [];

    public virtual List<ConfigurationM2Ssd> ConfigurationM2Ssds { get; set; } = [];
}

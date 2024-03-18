namespace PCConfigurator.Model.Components;

public class Socket()
{
    public int SocketId { get; set; }
    public string Name { get; set; }

    public virtual List<Chipset> Chipsets { get; } = [];
    public virtual List<Cooler> Coolers { get; } = [];
    public virtual List<Cpu> Cpus { get; } = [];
    public virtual List<Motherboard> Motherboards { get; } = [];

    public override string ToString()
    {
        return Name;
    }
}

namespace PCConfigurator.Model.Components;

public class Chipset
{
    public int ChipsetId { get; set; }
    public string Name { get; set; }

    public int SocketId { get; set; }
    public virtual Socket Socket { get; set; }

    public virtual List<Motherboard> Motherboards { get; } = [];

    public override string ToString()
    {
        return Name;
    }
}

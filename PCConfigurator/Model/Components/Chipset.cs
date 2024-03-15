namespace PCConfigurator.Model.Components;

public class Chipset
{
    public int ChipsetId { get; set; }
    public required string Name { get; set; }

    public int SocketId { get; set; }
    public virtual required Socket Socket { get; set; }

    public virtual List<Motherboard> Motherboards { get; } = [];

    public static Chipset FromString(string s)
    {
        ApplicationContext dbContext = new ApplicationContext();
        Chipset chipset = dbContext.Chipset.First();
        return chipset;
    }

    public override string ToString()
    {
        return Name;
    }
}

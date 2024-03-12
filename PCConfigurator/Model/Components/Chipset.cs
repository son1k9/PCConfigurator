namespace PCConfigurator.Model.Components;

internal class Chipset
{
    public int ChipsetId { get; set; }
    public required string Name { get; set; }

    public int SocketId { get; set; }
    public required Socket Socket { get; set; }

    public List<Motherboard> Motherboards { get; } = [];

    public static Chipset FromString(string s)
    {
        ApplicationContext dbContext = new ApplicationContext();
        Chipset chipset = dbContext.Chipsets.First();
        return chipset;
    }
}


namespace PCConfigurator.Model.Components;

public class Ram
{
    public int RamID { get; set; }
    public string Model { get; set; }
    public RamType RamType { get; set; }
    public int Capacity { get; set; } = 1;
    public int Clock { get; set; } = 800;
    public int Cl { get; set; } = 5;

    public virtual List<Configuration> Configurations { get; } = [];

    public Ram Clone()
    {
        return new Ram()
        {
            RamID = RamID,
            Model = Model,
            RamType = RamType,
            Capacity = Capacity,
            Clock = Clock,
            Cl = Cl
        };
    }
}

using System.Collections.ObjectModel;

namespace PCConfigurator.Model.Components;

public class Socket()
{
    public int SocketId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Chipset> Chipsets { get; set; } = new ObservableCollection<Chipset>();

    public virtual List<Cooler> Coolers { get; set; } = [];

    public virtual List<Cpu> Cpus { get; set;  } = [];

    public virtual List<Motherboard> Motherboards { get; set; } = [];


    public override string ToString()
    {
        return Name;
    }

    public Socket Clone()
    {
        ObservableCollection<Chipset> collection = [];
        foreach (Chipset chipset in Chipsets)
        {
            collection.Add(chipset);
        }

        return new Socket()
        {
            SocketId = SocketId,
            Name = Name,
            Coolers = Coolers,
            Cpus = Cpus,
            Motherboards = Motherboards,
            Chipsets = collection
        };
    }
}
